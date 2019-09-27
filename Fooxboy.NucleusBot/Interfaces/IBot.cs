using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.NucleusBot.Interfaces
{
    public interface IBot
    {
        List<INucleusCommand> Commands { get; set; }
        Dictionary<string, string> AliasesCommand { get; set; }
        void Start();
        void Stop();
    }
}
