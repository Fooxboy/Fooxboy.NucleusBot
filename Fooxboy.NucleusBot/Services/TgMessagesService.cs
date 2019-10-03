using System;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Fooxboy.NucleusBot.Services
{
    public class TgMessagesService : IGetUpdateService
    {
        public event NewMessageDelegate NewMessageEvent;
        public event NewMessageDelegate NewMessageReplyEvent;
        public event NewMessageDelegate MessageEditEvent;
        public event UserMessageAction MessageAllowEvent;
        public event UserMessageAction MessageDenyEvent;
        public event UserMessageAction GroupLeaveEvent;

        private ITelegramBotClient Bot;
        private IBotSettings _settings;
        private ILoggerService _logger;
        private bool _isStarted;

        public TgMessagesService(IBotSettings settings, ILoggerService logger)
        {
            _settings = settings;
            _logger = logger;
        }
        public void Start()
        {
            if (_settings.TGToken == null)
            {
                _logger.Error("Не указан токен для Telegram");
                throw new ArgumentNullException("Вы не указали токен для Telegram");
            }

            if (_isStarted)
            {
                _logger.Error("Telegram-бот уже работает");
                throw new ArgumentNullException("Telegram-бот уже работает");
            }

            _isStarted = true;
            Bot = new TelegramBotClient(_settings.TGToken);
            Bot.OnMessage += MessageProcessor;
            Bot.OnCallbackQuery += CallbackQueryProcessor;
            Bot.StartReceiving();
        }

        public void MessageProcessor(object sender, MessageEventArgs e)
        {
            if (!_isStarted) return;
            var model = new Message();
            model.Platform = Enums.MessengerPlatform.Telegam;
            model.MessageTG = e.Message;
            model.ChatId = e.Message.Chat.Id;
            model.Text = e.Message.Text;
            NewMessageEvent.Invoke(model);
        }

        void CallbackQueryProcessor(object sender, CallbackQueryEventArgs e)
        {
            
        }

        public void Stop()
        {
            _logger.War("Остановка TelegramBot...");
            _isStarted = false;
            //throw new System.NotImplementedException();
        }
    }
}