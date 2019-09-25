using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.NucleusBot.Interfaces
{
    public interface ILoggerService
    {
        /// <summary>
        /// Отладочная информация
        /// </summary>
        /// <param name="text">Текст</param>
        void Trace(object text);
        /// <summary>
        /// Предупреждения
        /// </summary>
        /// <param name="text">Текст предупреждения</param>
        void War(object text);
        /// <summary>
        /// Некая информация
        /// </summary>
        /// <param name="text">Текст ифнормации</param>
        void Info(object text);
        /// <summary>
        /// Ошибка
        /// </summary>
        /// <param name="text">Текст ошибки</param>
        void Error(object text);
    }
}
