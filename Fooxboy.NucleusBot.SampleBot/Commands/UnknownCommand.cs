using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.NucleusBot.SampleBot.Commands
{
    public class UnknownCommand : INucleusCommand
    {
        public string Command => "unknown";

        public string[] Aliases => null;

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            sender.Text("Я не знаю такой команды, прости.", msg.ChatId);
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            logger.Trace("Этот код вызывается при старте бота :)");
        }
    }
}
