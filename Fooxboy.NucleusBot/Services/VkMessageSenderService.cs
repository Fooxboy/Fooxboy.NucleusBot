using Fooxboy.NucleusBot.Enums;
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
        private ILoggerService _logger;
        public static VkApi api;

        public MessengerPlatform Platform => MessengerPlatform.Vkontakte;

        public VkMessageSenderService(IBotSettings settings, ILoggerService logger)
        {
            _settings = settings;
            _logger = logger;
        }

        public void Image(string to, object image, string text = null, long from = 0)
        {
            throw new NotImplementedException();
        }

        public void Text(string text, long to, INucleusKeyboard keyboard = null, long from = 0)
        {
            api = api ?? new VkApi();
            api.Authorize(new ApiAuthParams()
            {
                AccessToken = _settings.VKToken
            });

            var readyKeyboard = new MessageKeyboard();
            readyKeyboard.OneTime = keyboard.OneTimeKeyboard;

            var builder = new VkNet.Model.Keyboard.KeyboardBuilder(keyboard.OneTimeKeyboard);
            foreach(var buttons in keyboard.)

            api.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams()
            {
                Keyboard = (MessageKeyboard)keyboard,
                Message = text,
                ChatId = to,
            });
        }
    }
}
