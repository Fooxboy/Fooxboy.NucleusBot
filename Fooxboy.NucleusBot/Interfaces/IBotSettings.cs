using Fooxboy.NucleusBot.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.NucleusBot.Interfaces
{
    public interface IBotSettings
    {
        /// <summary>
        /// Месседжер, который поддерживается ботом
        /// </summary>
        MessengerPlatform Messenger { get; set; }
        /// <summary>
        /// Токен сообщества ВКонтакте
        /// </summary>
        string VKToken { get; set; }
        /// <summary>
        /// Токен бота Telegram
        /// </summary>
        string TGToken { get; set; }
        /// <summary>
        /// Индентификатор группы ВКонтакте с ботом.
        /// </summary>
        long GroupId { get; set; }
    }
}
