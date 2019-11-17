using Fooxboy.NucleusBot.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.NucleusBot.Models
{
    public class Message
    {
        public MessengerPlatform Platform { get; set; }
        public string Text { get; set; }
        public PayloadNucleusBot Payload { get; set; }
        public long ChatId { get; set; }
        public VkNet.Model.Message MessageVK { get; set; }
        public string Trigger { get; set; }
        public Telegram.Bot.Types.Message MessageTG { get; set; }
        
    }
}
