using Fooxboy.NucleusBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Fooxboy.NucleusBot.Services
{
    public class TgMessageSenderService : IMessageSenderService
    {
        public static ITelegramBotClient tgbot;
        private IBotSettings _settings;

        public TgMessageSenderService(IBotSettings settings)
        {
            _settings = settings;
        }
        public void Image(string to, object image, string text = null, long from = 0)
        {
            throw new NotImplementedException();
        }

        public void Text(string text, long to, object keyboard = null, long from = 0)
        {
            tgbot= tgbot?? new TelegramBotClient(_settings.TGToken);
            var msgSendTask = tgbot.SendTextMessageAsync(
                chatId: to,
                text: text,
                replyMarkup: (ReplyKeyboardMarkup)keyboard
            );
        }
    }
}
