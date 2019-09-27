using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.NucleusBot.Interfaces
{
    public interface INucleusService
    {
        string Name { get; set; }
        bool IsRunning { get; }
        void Start(IBot bot, IBotSettings settings, List<IMessageSenderService> senders, ILoggerService logger);
        void Stop();
        
    }
}
