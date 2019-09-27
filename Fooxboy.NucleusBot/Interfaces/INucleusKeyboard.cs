
using System.Collections.Generic;

namespace Fooxboy.NucleusBot.Interfaces
{
    public interface INucleusKeyboard
    {
        List<List<INucleusKeyboardButton>> Buttons { get; set; }
        /// <summary>
        /// Если true, клавиатура будет скрыта сразу после использования
        /// </summary>
        bool OneTimeKeyboard { get; set; }
        /// <summary>
        /// Адаптивность клавиатуры: клиент подстроит высоту клавиатуры для наилучшего отображения (Только Telegram)
        /// </summary>
        bool ResizeKeyboard { get; set; }
        /// <summary>
        /// Отправка клавиатуры определенным пользователям (Только Telegram)
        /// </summary>
        bool Selective { get; set; }
    }
}