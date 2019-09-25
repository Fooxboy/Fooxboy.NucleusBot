using Fooxboy.NucleusBot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.NucleusBot.Interfaces
{
    public interface IGetUpdateService
    {
        /// <summary>
        /// Новое входящее сообщение.
        /// </summary>
        event NewMessageDelegate NewMessageEvent;

        /// <summary>
        /// Новое исходящее сообщение.
        /// </summary>
        event NewMessageDelegate NewMessageReplyEvent;

        /// <summary>
        /// Измененное сообщение.
        /// </summary>
        event NewMessageDelegate MessageEditEvent;

        /// <summary>
        /// Подписка на сообщение от сообщества.
        /// </summary>
        event UserMessageAction MessageAllowEvent;

        /// <summary>
        /// Пользователь запретил сообщения.
        /// </summary>
        event UserMessageAction MessageDenyEvent;

        /// <summary>
        /// Пользователь покинул сообщество.
        /// </summary>
        event UserMessageAction GroupLeaveEvent;
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
