using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.NucleusBot.Models
{
    public class UserMessagesActionModel
    {
        /// <summary>
        /// Индентификатор пользователя
        /// </summary>
        [JsonProperty("user_id")]
        public long UserId { get; set; }
        /// <summary>
        /// Переданный параметер в методе allowMessagesFromGroup
        /// </summary>
        [JsonProperty("key")]
        public long Key { get; set; }
        /// <summary>
        /// Указывает на то, что пользователь ушел сам или страница была удалена.
        /// </summary>
        [JsonProperty("self")]
        public long Self { get; set; }
        /// <summary>
        /// Указывает на то, как именно пользователь был добавлен:
        /// join - Вступил сам
        /// unsure - Выбрал вариант "возможно пойду" (для мероприятий)
        /// accepted - Пользователь принял приглашение
        /// approved - Заявка в группу была принята
        /// request - Пользователь подал заявку на вступление в сообщество.
        /// </summary>
        [JsonProperty("join_type")]
        public string JoinType { get; set; }
    }
}
