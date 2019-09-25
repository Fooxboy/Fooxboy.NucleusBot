using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.NucleusBot.Interfaces
{
    public interface IGroupSettings
    {
        string Token { get; set; }
        long GroupId { get; set; }
    }
}
