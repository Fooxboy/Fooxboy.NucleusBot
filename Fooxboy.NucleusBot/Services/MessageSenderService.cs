using Fooxboy.NucleusBot.Interfaces;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.NucleusBot.Services
{
    public class MessageSenderService : IMessageSenderService
    {
        private IBotSettings _settings;
        public MessageSenderService(IBotSettings settings)
        {
            _settings = settings;
        }
        public void Image(string to, string text = null, object image = null, long from = 0)
        {
            throw new NotImplementedException();
        }

        public void Text(string text, long to, object keyboard = null, long from = 0)
        {
            throw new NotImplementedException();
        }
    }
}
