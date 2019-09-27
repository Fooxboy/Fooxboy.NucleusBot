using Fooxboy.NucleusBot.Enums;
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
        private ILoggerService _logger;

        public MessengerPlatform Platform => MessengerPlatform.Telegam;

        public TgMessageSenderService(IBotSettings settings, ILoggerService logger)
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
            tgbot= tgbot?? new TelegramBotClient(_settings.TGToken);
            var msgSendTask = tgbot.SendTextMessageAsync(
                chatId: to,
                text: text,
                replyMarkup: (ReplyKeyboardMarkup)keyboard
            );
        }
    }
}
