using Fooxboy.NucleusBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fooxboy.NucleusBot.SampleBot.Services
{
    public class CounterService : INucleusService
    {
        public string Name => "Counter";

        public bool IsRunning { get; set; }

        public void Start(IBot bot, IBotSettings settings, List<IMessageSenderService> senders, ILoggerService logger)
        {
            //throw new NotImplementedException();
            IsRunning = true;
            while(IsRunning)
            {
                var counter = 0;
                Thread.Sleep(1000);
                counter++;
                logger.Trace(counter);
            }
        }


        public void Stop()
        {
            IsRunning = false;
        }
    }
}
