using Fooxboy.NucleusBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using VkNet;
using VkNet.Model;
using VkNet.Model.Keyboard;

namespace Fooxboy.NucleusBot.Services
{
    public class VkMessageSenderService: IMessageSenderService
    {
        private IBotSettings _settings;
        public static VkApi api;

        public VkMessageSenderService(IBotSettings settings)
        {
            _settings = settings;
        }

        public void Image(string to, object image, string text = null, long from = 0)
        {
            throw new NotImplementedException();
        }

        public void Text(string text, long to, object keyboard = null, long from = 0)
        {
            api = api ?? new VkApi();
            api.Authorize(new ApiAuthParams()
            {
                AccessToken = _settings.VKToken
            });

            api.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams()
            {
                Keyboard = (MessageKeyboard)keyboard,
                Message = text,
                ChatId = to,
            });
        }
    }
}
