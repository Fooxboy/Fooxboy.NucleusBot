using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using Fooxboy.NucleusBot.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Exception;
using VkNet.Model;

namespace Fooxboy.NucleusBot
{
    public class Bot:IBot
    {
        private IBotSettings _settings;
        private List<IGetUpdateService> _updaters;
        private ILoggerService _logger;
        private IProcessor _processor;
        private List<INucleusService> _botServices;

        /// <summary>
        /// Команда, которая вызывается, когда ядро не смогло найти в Ваших командах нужную.
        /// </summary>
        public INucleusCommand UnknownCommand { get; set; }
        /// <summary>
        /// Сервисы, которые реализовывают отправку сообщений в определённый месседжер
        /// </summary>
        public List<IMessageSenderService> SenderServices { get;  set; }
        /// <summary>
        /// Алиасы комманд.
        /// </summary>
        public Dictionary<string, PayloadNucleusBot> AliasesCommand { get; set; }
        /// <summary>
        /// Команды
        /// </summary>
        public List<INucleusCommand> Commands { get; set; }
        public string ScreenNameBot { get; set; }

        /// <summary>
        /// Bot
        /// </summary>
        /// <param name="settings">Настройки бота</param>
        /// <param name="unknownCommand">Команда, которая вызывается, когда ядро не смогло найти в Ваших командах нужную, если пусто - используется встроенная.</param>
        /// <param name="updaterServices">Сервивы получения сообщений, если пусто используются встроенные</param>
        /// <param name="senders">Сервивы отправки сообщений, если пусто используются встроенные</param>
        /// <param name="processor">Экзепляр класса обработки полученных сообщений, если пусто используется встроенный</param>
        /// <param name="logger">Экзепляр класса логгера, если пусто используется встроенный.</param>
        public Bot(IBotSettings settings, INucleusCommand unknownCommand = null, List<IGetUpdateService> updaterServices = null, List<IMessageSenderService> senders = null, IProcessor processor = null, ILoggerService logger = null)
        {
            Console.WriteLine("Fooxboy.NucleusBot Core. 2019. Версия: 0.1 alpha");
            Console.WriteLine("Инициалиация NucleusBot...");
            IKernel kernel = new StandardKernel(new NinjectConfigModule());
            _logger = logger?? new LoggerService();
            _botServices = new List<INucleusService>();
            _settings = settings;
            if (updaterServices == null)
            {
                var list = new List<IGetUpdateService>();
                if (_settings.Messenger == Enums.MessengerPlatform.Telegam) list.Add(new Services.TgMessagesService(_settings, _logger));
                else if (_settings.Messenger == Enums.MessengerPlatform.Vkontakte) list.Add(new Services.LongPollService(_settings, _logger));
                else if (_settings.Messenger == Enums.MessengerPlatform.VkontakteAndTelegram)
                {
                    list.Add(new LongPollService(_settings, _logger));
                    list.Add(new TgMessagesService(_settings, _logger));
                }
                _updaters = list;
            }
            else _updaters = updaterServices;
            if (senders == null)
            {
                var list = new List<IMessageSenderService>();
                if (_settings.Messenger == Enums.MessengerPlatform.Telegam) list.Add(new Services.TgMessageSenderService(_settings, _logger));
                else if (_settings.Messenger == Enums.MessengerPlatform.Vkontakte) list.Add(new Services.VkMessageSenderService(_settings, _logger));
                else if (_settings.Messenger == Enums.MessengerPlatform.VkontakteAndTelegram)
                {
                    list.Add(new TgMessageSenderService(_settings, _logger));
                    list.Add(new VkMessageSenderService(_settings, _logger));
                }
                SenderServices = list;
            }
            else SenderServices = senders;

            AliasesCommand = new Dictionary<string, PayloadNucleusBot>();
            UnknownCommand = unknownCommand ?? new UnknownCommand();
            _processor = processor ?? new Processor(_logger, this, kernel);
        }

        public ILoggerService GetLogger() => _logger;
        public void SetCommands(params INucleusCommand[] commands) => this.Commands = commands.ToList();

        public void SetServices(params INucleusService[] services)=> this._botServices = services.ToList();
        /// <summary>
        /// Запустить бота.
        /// </summary>
        public void Start()
        {
            _logger.Trace("Запуск бота...");

            if (this.Commands == null) throw new ArgumentNullException("Команды не были инициализированны. Используйте метод SetCommands.");
            foreach (var command in this.Commands)
            {
                _logger.Trace($"Инициализация команды {command.Command}...");
                if(command.Aliases != null) foreach (var alias in command.Aliases) AliasesCommand.Add(alias, new PayloadBuilder(command.Command).BuildToModel());
                try
                {
                    command.Init(this, _logger);
                }catch(Exception e)
                {
                    _logger.Error($"Произошла ошибка при инициализации команды {command.Command}: \n {e}");
                }
            }

            if(_settings.Messenger == Enums.MessengerPlatform.VkontakteAndTelegram || _settings.Messenger == Enums.MessengerPlatform.Vkontakte)
            {
                var vkapi = new VkApi();
                vkapi.Authorize(new ApiAuthParams()
                {
                    AccessToken = _settings.VKToken
                });

                var info =  vkapi.Groups.GetById(new List<string>() { _settings.GroupId.ToString() }, groupId: _settings.GroupId.ToString(), GroupsFields.Description);
                this.ScreenNameBot = info[0].ScreenName;
            }
            

            foreach (var updater in _updaters)
            {
                updater.NewMessageEvent += NewMessage;
                Task.Run(() => updater.Start());
            }

            foreach(var service in this._botServices)
            {
                _logger.Trace($"Запуск сервиса {service.Name}...");
                Task.Run(() => service.Start(this, _settings, SenderServices, _logger)); 
            }
        }

        private void NewMessage(Models.Message message)=> Task.Run(() => _processor.Start(message));

        public void Stop()
        {
            _logger.War("Остановка сервисов получения обновлений...");
            foreach(var serivceUpdate in _updaters) serivceUpdate.Stop();
            _logger.War("Остановка сервисов получения обновлений завершена.");
            //throw new NotImplementedException();
        }
    }
}
