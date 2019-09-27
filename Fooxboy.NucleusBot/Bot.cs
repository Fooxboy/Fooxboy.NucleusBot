using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using Fooxboy.NucleusBot.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private IProcessor _processor;
        private List<INucleusService> _botServices;

        public List<IMessageSenderService> SenderServices { get;  set; }
        public Dictionary<string, string> AliasesCommand { get; set; }
        public List<INucleusCommand> Commands { get; set; }

        public Bot(IBotSettings settings, List<IGetUpdateService> updaterServices = null, List<IMessageSenderService> senders = null, IProcessor processor = null, ILoggerService logger = null)
        {
            Console.WriteLine("Fooxboy.NucleusBot. 2019. Версия: 0.1 alpha");
            Console.WriteLine("Инициалиация NucleusBot...");
            IKernel kernel = new StandardKernel(new NinjectConfigModule());
            _logger = logger?? new LoggerService();
            _settings = settings;

            if (updaterServices == null)
            {
                var list = new List<IGetUpdateService>();
                if (_settings.Messenger == Enums.MessengerPlatform.Telegam) list.Add(new Services.TgMessagesService(_settings, _logger));
                else if (_settings.Messenger == Enums.MessengerPlatform.Vkontakte) list.Add(new Services.LongPollService(_settings, _logger));
                else if (_settings.Messenger == Enums.MessengerPlatform.VkontakteAndTelegram)
                {
                    list.Add(new Services.LongPollService(_settings, _logger));
                    list.Add(new Services.TgMessagesService(_settings, _logger));
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
                    list.Add(new Services.TgMessageSenderService(_settings, _logger));
                    list.Add(new Services.VkMessageSenderService(_settings, _logger));
                }
                SenderServices = list;
            }
            else SenderServices = senders;

            AliasesCommand = new Dictionary<string, string>();
            _processor = processor ?? new Processor(_logger, this, kernel);
        }


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
                foreach (var alias in command.Aliases) AliasesCommand.Add(alias, command.Command);
                command.Init(this);
            }

            foreach (var updater in _updaters)
            {
                updater.NewMessageEvent += NewMessage;
                Task.Run(() => updater.Start());
            }

            foreach(var service in this._botServices)
            {
                _logger.Trace($"Запуск сервиса {service.Name}");
                Task.Run(() => service.Start(this, _settings, SenderServices, _logger)); 
            }
        }

        private void NewMessage(Models.Message message)=> Task.Run(() => _processor.Start(message));

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
