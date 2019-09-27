using Fooxboy.NucleusBot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.NucleusBot.Interfaces
{
    public interface INucleusCommand
    {
        /// <summary>
        /// Текст на который бот будет реагировать на команду.
        /// </summary>
        string Command { get; }
        string[] Aliases { get; }

        /// <summary>
        /// Метод, который будет выполнятся, когда пользователь напишит команду 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="sender"></param>
        void Execute(Message msg, IMessageSenderService sender, IBot bot);

        /// <summary>
        /// Метод, который вызывается при запуске для инициализации. Может быть пустым.
        /// </summary>
        void Init(IBot bot, ILoggerService logger);
    }
}
