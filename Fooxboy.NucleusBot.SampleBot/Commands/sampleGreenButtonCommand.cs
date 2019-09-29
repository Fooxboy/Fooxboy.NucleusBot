﻿using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;

namespace Fooxboy.NucleusBot.SampleBot.Commands
{
    public class sampleGreenButtonCommand : INucleusCommand
    {
        public string Command => "sampleGreenButtonCommand";
        public string[] Aliases => new[] {"Зеленая кнопка"};
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            sender.Text("Ты нажал на зеленую кнопку!", msg.ChatId);
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            logger.Trace("Команда sampleGreenButtonCommand инициализирована");
        }
    }
}