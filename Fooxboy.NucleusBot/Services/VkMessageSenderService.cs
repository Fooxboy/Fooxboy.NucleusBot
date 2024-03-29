﻿using Fooxboy.NucleusBot.Enums;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Model;
using VkNet.Model.Keyboard;

namespace Fooxboy.NucleusBot.Services
{
    public class VkMessageSenderService : IMessageSenderService
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

        private MessageKeyboard ConvertToVkKeyboard(INucleusKeyboard keyboard)
        {
            var keyboardVkButtons = new List<List<MessageKeyboardButton>>();
            
            foreach (var buttonsLine in keyboard.Buttons)
            {
                var line = new List<MessageKeyboardButton>();
                foreach (var button in buttonsLine)
                {
                    var vkButton = new MessageKeyboardButton();
                    vkButton.Action = new MessageKeyboardButtonAction()
                    {
                        AppId = button.AppID ==0? null: button?.AppID,
                        Hash = button.Hash,
                        Label = button.Caption,
                        OwnerId = button.OwnerID == 0? null: (button?.OwnerID as long?),
                        Payload = new PayloadBuilder().BuildToStringFromModel(button.Payload),
                        Type = button.Type,
                    };

                    vkButton.Color = button.Color;
                    line.Add(vkButton);
                }
                keyboardVkButtons.Add(line);
            }

            var vkKeyboard = new MessageKeyboard();
            vkKeyboard.Buttons = keyboardVkButtons;
            vkKeyboard.OneTime = keyboard.OneTimeKeyboard;
            return vkKeyboard;
        }

        public void Text(string text, long to, INucleusKeyboard keyboard = null, long from = 0)
        {
            api = api ?? new VkApi();
            api.Authorize(new ApiAuthParams()
            {
                AccessToken = _settings.VKToken
            });
            var vkKeyboard = keyboard != null ? ConvertToVkKeyboard(keyboard) : null;
            Task.Run(() =>
            {
                api.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams()
                {
                    Keyboard = vkKeyboard,
                    Message = text,
                    PeerId = to,
                    RandomId = new Random().Next(0, 999999)
                });
            });
        }
    }
}
