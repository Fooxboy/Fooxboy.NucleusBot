using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using Fooxboy.NucleusBot.Services;
using Newtonsoft.Json;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VkNet.Model.Attachments;

namespace Fooxboy.NucleusBot
{
    public class Processor : IProcessor
    {
        ILoggerService _logger;
        IBot _bot;
        IKernel _kernel;
        public Processor(ILoggerService logger, IBot bot, IKernel kernel)
        {
            _logger = logger;
            _bot = bot;
            _kernel = kernel;
        }
        public void Start(Message message)
        {
            _logger.Trace("Начало обработки сообщения.");
            var commandString = string.Empty;
            if(message.Platform == Enums.MessengerPlatform.Telegam) commandString = message.MessageTG.Text.Split(' ')[0];
            else if(message.Platform == Enums.MessengerPlatform.Vkontakte)
            {
                if (message.MessageVK.Payload == null) commandString = message.MessageVK.Text.Split(' ')[0];
                else
                {
                    var payload = JsonConvert.DeserializeObject<PayloadNucleusBot>(message.MessageVK.Payload);
                    commandString = payload.Command;
                }
            }
            var command = SearchCommand(commandString);
            if (command is null) return;
        }

        private INucleusCommand SearchCommand(string commandString)
        {
            _logger.Trace("Поиск команды...");
            var command = _bot.Commands.Find(c => c.Command.ToLower() == commandString.ToLower());
            if (command == null)
            {
                var commandStr = string.Empty;
                foreach(var alias in _bot.AliasesCommand) if (alias.Key == commandString) commandStr = alias.Value;
                if (commandStr != null) command = _bot.Commands.Find(c => c.Command.ToLower() == commandString.ToLower());
            }
            if (command == null) return _bot.UnknownCommand;
            return command;
        }

        private void ExecuteCommand(INucleusCommand command, Message msg)
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            IMessageSenderService sender = _bot.SenderServices.Single(s => s.Platform == msg.Platform);
            try
            {
                command.Execute(msg, sender, _bot);
            }catch(Exception e)
            {
                _logger.Error($"Произошла ошибка в команде {command.Command}: \n {e}");
            }
            
            sw.Stop();
            _logger.Trace($"Команда {command.Command} выполнялась {sw.ElapsedMilliseconds} ms");
        }
    }
}
