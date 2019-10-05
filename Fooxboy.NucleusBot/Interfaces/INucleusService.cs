using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.NucleusBot.Interfaces
{
    public interface INucleusService
    {
        string Name { get; }
        bool IsRunning { get; set; }
        void Start(IBot bot, IBotSettings settings, List<IMessageSenderService> senders, ILoggerService logger);
        void Stop();
        
    }
}
