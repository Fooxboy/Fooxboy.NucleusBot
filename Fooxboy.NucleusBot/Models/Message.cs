using Fooxboy.NucleusBot.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.NucleusBot.Models
{
    public class Message: VkNet.Model.Message
    {
        public MessengerPlatform Platform { get; set; }
        public PayloadNucleusBot PayloadNucleusBot { get; set; }
    }
}
