using Fooxboy.NucleusBot.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.NucleusBot.Interfaces
{
    public interface IMessageSenderService
    {
        void Text(string text, long to, MessengerPlatform platform, object keyboard = null, long from = 0);
        void Image(string to, object image, MessengerPlatform platform, string text = null, long from = 0);
    }
}
