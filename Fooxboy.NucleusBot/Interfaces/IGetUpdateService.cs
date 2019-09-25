using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.NucleusBot.Interfaces
{
    public interface IGetUpdateService
    {
        /// <summary>
        /// Начало получения новых сообщений
        /// </summary>
        void Start();
        /// <summary>
        /// Остановить получение новых сообщений.
        /// </summary>
        void Stop();
    }
}
