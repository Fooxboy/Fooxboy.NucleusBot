using Fooxboy.NucleusBot.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.NucleusBot.Interfaces
{
    public interface IBotSettings
    {
        MessengerPlatform Messenger { get; set; }
        string Token { get; set; }
        long GroupId { get; set; }
    }
}
