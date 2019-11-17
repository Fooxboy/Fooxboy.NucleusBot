using Fooxboy.NucleusBot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.NucleusBot.Interfaces
{
    public interface IBot
    {
        string ScreenNameBot { get; set; }
        List<INucleusCommand> Commands { get; set; }
        List<IMessageSenderService> SenderServices { get; set; }
        INucleusCommand UnknownCommand { get; set; }
        Dictionary<string, PayloadNucleusBot> AliasesCommand { get; set; }
        void Start();
        void Stop();

        void SetCommands(params INucleusCommand[] commands);
        void SetServices(params INucleusService[] services);
    }
}
