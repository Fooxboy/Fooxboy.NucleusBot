using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Fooxboy.NucleusBot.Services
{
    /// <summary>
    /// Сервис для получения обновлений с ВКонтакте для групп.
    /// </summary>
    public class LongPollService : IGetUpdateService
    {
        /// <summary>
        /// Новое входящее сообщение.
        /// </summary>
        public event NewMessageDelegate NewMessageEvent;

        /// <summary>
        /// Новое исходящее сообщение.
        /// </summary>
        public event NewMessageDelegate NewMessageReplyEvent;

        /// <summary>
        /// Измененное сообщение.
        /// </summary>
        public event NewMessageDelegate MessageEditEvent;

        /// <summary>
        /// Подписка на сообщение от сообщества.
        /// </summary>
        public event UserMessageAction MessageAllowEvent;

        /// <summary>
        /// Пользователь запретил сообщения.
        /// </summary>
        public event UserMessageAction MessageDenyEvent;

        /// <summary>
        /// Пользователь покинул сообщество.
        /// </summary>
        public event UserMessageAction GroupLeaveEvent;

        /// <summary>
        /// Пользователь вступил в сообщество.
        /// </summary>
        public event UserMessageAction GroupJoinEvent;


        private IBotSettings _settings;
        private ILoggerService _logger;
        private bool _isStart;
        private string _server;
        private string _key;
        private long _ts;
        public LongPollService(IBotSettings settings, ILoggerService logger)
        {
            _settings = settings;
            _logger = logger;
        }
        public void Start()
        {
            if (_settings.VKToken == null || _settings.VKToken == "")
            {
                _logger.Error("Не был указан токен");
                throw new ArgumentNullException("Вы не указали токен");
            }

            if(_settings.GroupId == 0)
            {
                _logger.Error("Не был указан ID сообщества");
                throw new ArgumentNullException("Вы не указали ID сообщества");
            }

            if(_isStart)
            {
                _logger.Error("Longpoll уже работает");
                throw new ArgumentNullException("Longpoll уже работает");
            }

            _isStart = true;
            ResetSettingsLongPoll();
            SetSettingLongPoll();
            SeriesLongPoll();
        }

        private void SeriesLongPoll()
        {
            while(_isStart)
            {
                try
                {
                    var json = Request();
                    if (json == null) goto End;
                    var response = JsonConvert.DeserializeObject<RootLongPollModel>(json);
                    if (response.Ts == 0)
                    {
                        _logger.Error("Возникла ошибка в longPoll. Получаю новые server и ts...");
                        ResetSettingsLongPoll();
                        SetSettingLongPoll();
                    }

                    _ts = response.Ts;
                    _logger.Trace("Обработка полученных сообщений.");
                    _logger.Trace($"Получено обновлений: {response.Updates.Count}");
                    foreach(var update in response.Updates)
                    {
                        var type = update.Type;

                        if(type == "message_new")
                        {
                            var model = new Message();
                            var obj = (JObject)update.Object;
                            var message = obj.ToObject<VkNet.Model.Message>();
                            model.Platform = Enums.MessengerPlatform.Vkontakte;
                            model.MessageVK = message;
                            NewMessageEvent?.Invoke(model);
                        }else if(type == "message_reply")
                        {
                            var model = new Message();
                            var obj = (JObject)update.Object;
                            var message = obj.ToObject<VkNet.Model.Message>();
                            model.Platform = Enums.MessengerPlatform.Vkontakte;
                            model.MessageVK = message;
                            NewMessageReplyEvent?.Invoke(model);
                        }else if(type == "message_edit")
                        {
                            var model = new Message();
                            var obj = (JObject)update.Object;
                            var message = obj.ToObject<VkNet.Model.Message>();
                            model.Platform = Enums.MessengerPlatform.Vkontakte;
                            model.MessageVK = message;
                            MessageEditEvent?.Invoke(model);
                        }else if(type == "message_allow")
                        {
                            var obj = (JObject)update.Object;
                            var model = obj.ToObject<UserMessagesActionModel>();
                            MessageAllowEvent?.Invoke(model);
                        }else if(type == "message_deny")
                        {
                            var obj = (JObject)update.Object;
                            var model = obj.ToObject<UserMessagesActionModel>();
                            MessageDenyEvent?.Invoke(model);
                        }else if(type == "group_leave")
                        {
                            var obj = (JObject)update.Object;
                            var model = obj.ToObject<UserMessagesActionModel>();
                            GroupLeaveEvent?.Invoke(model);
                        }else if(type == "group_join")
                        {
                            var obj = (JObject)update.Object;
                            var model = obj.ToObject<UserMessagesActionModel>();
                            GroupJoinEvent?.Invoke(model);
                        }else
                        {
                            _logger.Error($"Тип обновления {type} пока что не поддерживается. Отключите его получение в настройках сообщества, если это сообщение Вам мешает.");
                        }
                    }

                End:
                    var i = 1;
                    //_logger.Trace("\n");
                }
                catch (Exception e) 
                {
                    _logger.Error($"Произошла ошибка при обработке обновлений:\n {e.ToString()}");
                }
            }
        }

        private void ResetSettingsLongPoll()
        {
            _logger.Trace("Сброс настроек для Longpoll...");
            _server = string.Empty;
            _key = string.Empty;
            _ts = 0;
        }

        private void SetSettingLongPoll()
        {
            _logger.Trace("Установка параметров Longpoll");
            var json = string.Empty;

            using (var client = new WebClient())
            {
                json = client.DownloadString($"https://api.vk.com/method/groups.getLongPollServer?group_id={_settings.GroupId}&access_token={_settings.VKToken}&v=5.92");
            }

            var model = JsonConvert.DeserializeObject<Models.KeyAndServerModel>(json);
            _key = model.response.Key;
            _server = model.response.Server;
            _ts = model.response.Ts;
            _logger.Trace("Установка параметров завершена.");
        }

        private string Request()
        {
            try
            {
                _logger.Trace("Запрос к серверу ВКонтакте...");
                var url = $"{_server}?act=a_check&key={_key}&ts={_ts}&wait=20";
                _logger.Trace($"Запрос по url: {url}");
                var json = string.Empty;
                var request = HttpWebRequest.Create(url);
                request.Timeout = 25000;
                var response = request.GetResponse();
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        json = reader.ReadToEnd();
                    }
                }
                _logger.Trace("Ответ от ВКонтакте получен.");
                return json;
            }catch(Exception e)
            {
                _logger.Error($"Произошла ошибка при запросе:\n {e.ToString()}");
                return null;
            }
        }

        public void Stop()
        {
            _logger.War("Остановка LongPoll ВКонтакте...");
            _isStart = false;
            //throw new NotImplementedException();
        }
    }
}
