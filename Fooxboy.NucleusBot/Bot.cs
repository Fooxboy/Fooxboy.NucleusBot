using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fooxboy.NucleusBot
{
    public class Bot:IBot
    {
        private IGroupSettings _settings;
        private IGetUpdateService _updater;
        private ILoggerService _logger;
        private IMessageSenderService _sender;
        private IProcessor _processor;
        public Bot(IGroupSettings settings, IGetUpdateService updaterService = null, IMessageSenderService sender = null, IProcessor processor = null, ILoggerService logger = null)
        {
            Console.WriteLine("Fooxboy.NucleusBot. 2019. Версия: 0.1 alpha");
            Console.WriteLine("Инициалиация NucleusBot...");
            _logger = logger?? new LoggerService();
            _settings = settings;
            _updater = updaterService?? new LongPollService(_settings, _logger);
            _sender = sender ?? new MessageSenderService(_settings);
            _processor = processor ?? new Processor(_logger, this);
        }

        /// <summary>
        /// Запустить бота.
        /// </summary>
        public void Start()
        {
            _logger.Trace("Запуск бота...");
            _updater.NewMessageEvent += NewMessage;
            Task.Run(() => _updater.Start());
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
