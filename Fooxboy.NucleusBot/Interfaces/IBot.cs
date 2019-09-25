using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.NucleusBot.Interfaces
{
    public interface IBot
    {
        List<INucleusCommand> Commands { get; set; }
        void Start();
        void Stop();
    }
}
