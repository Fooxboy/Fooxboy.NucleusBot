using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.NucleusBot.Interfaces
{
    public interface INucleusService
    {
        void Start(IBot bot, IBotSettings settings, IMessageSenderService sender, ILoggerService logger);
        void Stop();
        
    }
}
