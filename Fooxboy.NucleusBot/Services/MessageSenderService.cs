using Fooxboy.NucleusBot.Enums;
using Fooxboy.NucleusBot.Interfaces;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using VkNet;

namespace Fooxboy.NucleusBot.Services
{
    public class MessageSenderService : IMessageSenderService
    {
        private IBotSettings _settings;

        public static VkApi api;
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
            if(platform == MessengerPlatform.Vkontakte)
            {
                api = api ?? new VkApi();
                
            }
            
            //throw new NotImplementedException();
        }
    }
}
