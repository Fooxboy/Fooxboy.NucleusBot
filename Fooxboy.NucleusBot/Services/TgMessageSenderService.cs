using Fooxboy.NucleusBot.Enums;
using Fooxboy.NucleusBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
            var replyKeyboardMarkup = new ReplyKeyboardMarkup();
            if (keyboard != null)
            {
                var keyboardArray = new List<List<KeyboardButton>>();
                foreach (var buttons in keyboard.Buttons)
                {
                    var line = new List<KeyboardButton>();
                    foreach (var button in buttons)
                    {
                        line.Add(new KeyboardButton()
                        {
                            RequestContact = button.RequestContact,
                            RequestLocation = button.RequestLocation,
                            Text = button.Caption
                        });
                        keyboardArray.Add(line);
                        line.Clear();
                    }
                }
                replyKeyboardMarkup.Keyboard = keyboardArray;
                replyKeyboardMarkup.ResizeKeyboard = keyboard.ResizeKeyboard;
                replyKeyboardMarkup.OneTimeKeyboard = keyboard.OneTimeKeyboard;
                Task.Run(() =>
                {
                    tgbot.SendTextMessageAsync(
                        chatId: to,
                        text: text,
                        replyMarkup: replyKeyboardMarkup
                    );
                });
            }
            else
            {
                Task.Run(() =>
                {
                    tgbot.SendTextMessageAsync(
                        chatId: to,
                        text: text
                    );
                });
            }

            
        }

        public void Keyboard(INucleusKeyboard keyboard)
        {
            
        }
    }
}
