using Fooxboy.NucleusBot.Enums;
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
        public void Image(string to, object image, MessengerPlatform platform, string text = null, long from = 0)
        {
            throw new NotImplementedException();
        }

        public void Text(string text, long to, MessengerPlatform platform,  object keyboard = null, long from = 0)
        {
            throw new NotImplementedException();
        }
    }
}
