using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using Fooxboy.NucleusBot.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VkNet.Exception;
using VkNet.Model;

namespace Fooxboy.NucleusBot
{
    public class Bot:IBot
    {
        private IBotSettings _settings;
        private List<IGetUpdateService> _updaters;
        private ILoggerService _logger;
        private IMessageSenderService _sender;
        private IProcessor _processor;

        public List<INucleusCommand> Commands { get; set; }

        public Bot(IBotSettings settings, List<IGetUpdateService> updaterServices = null, IMessageSenderService sender = null, IProcessor processor = null, ILoggerService logger = null)
        {
            Console.WriteLine("Fooxboy.NucleusBot. 2019. Версия: 0.1 alpha");
            Console.WriteLine("Инициалиация NucleusBot...");
            IKernel kernel = new StandardKernel(new NinjectConfigModule());
            _logger = logger?? new LoggerService();
            _settings = settings;

            if(updaterServices == null)
            {
                var list = new  List<IGetUpdateService>();
                if (_settings.Messenger == Enums.MessengerPlatform.Telegam) list.Add(null);
                else if (_settings.Messenger == Enums.MessengerPlatform.Vkontakte) list.Add(new Services.LongPollService(_settings, _logger));
                else if(_settings.Messenger == Enums.MessengerPlatform.VkontakteAndTelegram)
                {
                    list.Add(new Services.LongPollService(_settings, _logger));
                    list.Add(null);
                }
            }
            _sender = sender ?? new MessageSenderService(_settings);
            _processor = processor ?? new Processor(_logger, this, kernel);
        }

        /// <summary>
        /// Запустить бота.
        /// </summary>
        public void Start()
        {
            _logger.Trace("Запуск бота...");

            foreach(var updater in _updaters)
            {
                updater.NewMessageEvent += NewMessage;
                Task.Run(() => updater.Start());
            }
        }

        private void NewMessage(Models.Message message)
        {
            Task.Run(() =>
            {
                _logger.Trace($"Новое сообщение: {message.Text}");
                _processor.Start(message);
            });
        }

        public void Stop()
        {
            //throw new NotImplementedException();
        }
    }
}
