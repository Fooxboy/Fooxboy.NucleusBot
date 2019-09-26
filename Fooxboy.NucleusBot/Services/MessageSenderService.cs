using Fooxboy.NucleusBot.Enums;
using Fooxboy.NucleusBot.Interfaces;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using VkNet;
using VkNet.Model;
using VkNet.Model.Keyboard;

namespace Fooxboy.NucleusBot.Services
{
    public class MessageSenderService : IMessageSenderService
    {
        private IBotSettings _settings;
        private ITelegramBotClient tgbot;
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
                api.Authorize(new ApiAuthParams()
                {
                    AccessToken = _settings.VKToken
                }) ;

                api.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams()
                {
                    Keyboard = (MessageKeyboard)keyboard,
                    Message = text,
                    ChatId = to,
                });
            }
            else
            {
                tgbot = new TelegramBotClient(_settings.TGToken);
                var msgSendTask = tgbot.SendTextMessageAsync(
                    chatId: to,
                    text: text,
                    replyMarkup: (ReplyKeyboardMarkup)keyboard
                );
            }
            
            //throw new NotImplementedException();
        }
    }
}
