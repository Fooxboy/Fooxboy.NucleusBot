using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using Fooxboy.NucleusBot.Services;
using Newtonsoft.Json;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fooxboy.NucleusBot.Enums;
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
            if(message.Platform == Enums.MessengerPlatform.Telegam)
            {
                commandString = message.MessageTG.Text;
                message.Text = message.MessageTG.Text;
                message.ChatId = message.MessageTG.Chat.Id;
                foreach (var alias in _bot.AliasesCommand)
                {
                    if (alias.Key.ToLower() == commandString.ToLower()) message.Payload = alias.Value;
                }
                
            }
            else if(message.Platform == Enums.MessengerPlatform.Vkontakte)
            {
                message.Text = message.MessageVK.Text;
                message.ChatId = message.MessageVK.ChatId ?? message.MessageVK.PeerId.Value;
                if (message.MessageVK.Payload == null) commandString = message.MessageVK.Text.Split(' ')[0];
                else
                {
                    var payload = JsonConvert.DeserializeObject<PayloadNucleusBot>(message.MessageVK.Payload);
                    message.Payload = payload;
                    commandString = payload.Command;
                }
            }
            if (commandString.Split(' ')[0] == $"@{_bot.ScreenNameBot}") commandString = commandString.Split(' ').Length > 1?  commandString.Split(' ')[1]: "1";
            var command = SearchCommand(commandString, message.Platform);
            if (command is null) return;
            ExecuteCommand(command, message);
        }

        private INucleusCommand SearchCommand(string commandString, MessengerPlatform platform)
        {
            _logger.Trace("Поиск команды...");
            var command = _bot.Commands.Find(c => c.Command.ToLower() == commandString.ToLower());
            if (command == null)
            {
                var commandStr = string.Empty;
                foreach (var alias in _bot.AliasesCommand)
                {
                    if (alias.Key.ToLower() == commandString.ToLower())
                    {
                        commandStr = alias.Value.Command.ToLower();

                    }
                }
                if (commandStr != string.Empty) command = _bot.Commands.Find(c => c.Command.ToLower() == commandStr.ToLower());
            }

            if (command == null)
            {
                if (platform == MessengerPlatform.Telegam)
                {
                    command = _bot.Commands.Find(c => c.Command.ToLower() == commandString.Split(' ')[0].ToLower());
                    if(command == null) return _bot.UnknownCommand;
                }else return _bot.UnknownCommand;
                
            }
            return command;
        }

        private void ExecuteCommand(INucleusCommand command, Message msg)
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            IMessageSenderService sender = _bot.SenderServices.Single(s => s.Platform == msg.Platform);
            try
            {
                msg.Trigger = command.Command;
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
