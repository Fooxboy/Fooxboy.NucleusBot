using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using Fooxboy.NucleusBot.Services;
using Newtonsoft.Json;
using Ninject;
using System;
using System.Collections.Generic;
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
            if (message.Payload == null) commandString = message.Text.Split(' ')[0];
            else
            {
                message.PayloadNucleusBot = JsonConvert.DeserializeObject<PayloadNucleusBot>(message.Payload);
                commandString = message.PayloadNucleusBot.Command;
            }
            var command = SearchCommand(commandString);
            if (command is null) return;
        }

        private INucleusCommand SearchCommand(string command)
        {
            _logger.Trace("Поиск команды...");
            return _bot.Commands.Find(c => c.Command.ToLower() == command.ToLower());
        }

        private void ExecuteCommand(INucleusCommand command, Message msg)
        {
            var sw = new System.Diagnostics.Stopwatch();
            IMessageSenderService sender = _kernel.Get<MessageSenderService>();
            sw.Start();
            command.Execute(msg, sender, _bot);
            sw.Stop();
            _logger.Trace($"Команда {command.Command} выполнялась {sw.ElapsedMilliseconds} ms");
        }
    }
}
