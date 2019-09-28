using Fooxboy.NucleusBot.Enums;
using Fooxboy.NucleusBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.NucleusBot.Models
{
    public class BotSettings : IBotSettings
    {
        public MessengerPlatform Messenger { get; set; }
        public string VKToken { get; set; }
        public string TGToken { get; set; }
        public long GroupId { get; set; }
    }
}
