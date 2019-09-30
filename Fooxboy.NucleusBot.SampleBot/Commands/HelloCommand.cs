using System;
using System.Collections.Generic;
using System.Text;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;

namespace Fooxboy.NucleusBot.SampleBot.Commands
{
    public class HelloCommand : INucleusCommand
    {
        public string Command => "hello";
        public string[] Aliases => new string[]{"start","letItBegin"};
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            sender.Text("О хай Марк", msg.ChatId);
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            logger.Trace("Команда hello инициализирована");
        }
    }
}
