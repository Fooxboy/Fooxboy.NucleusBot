using System;
using System.Collections.Generic;
using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using VkNet.Enums.SafetyEnums;

namespace Fooxboy.NucleusBot
{
    public class KeyboardBuilder
    {
        private readonly List<List<INucleusKeyboardButton>> _fullKeyboard = new List<List<INucleusKeyboardButton>>();
        private List<INucleusKeyboardButton> _currentLine = new List<INucleusKeyboardButton>();
        private bool _oneTimeKb;
        private bool _resizeKb;
        private bool _isSelective;
        private IBot _bot;

        public KeyboardBuilder(IBot bot)
        {
            this._bot = bot;
        }

        public KeyboardBuilder AddButton(string text, string command, List<string> arguments = null, bool reqContact= false, bool reqLocation = false, KeyboardButtonColor color = default, KeyboardButtonActionType type = null,
            string btnHash = null, ulong? btnAppId = null, ulong? btnOwnerId = null)
        {

            if (command != null)
            {
                try
                {
                    var pb = new PayloadBuilder(command, arguments);
                    _bot.AliasesCommand.Add(text, pb.BuildToModel());
                }catch(Exception)
                {
                    //Console.WriteLine($"Алиас {text} уже существует.");
                }
            }
            AddButton(new NucleusKeyboardButton()
            {
                Caption = text ?? "my button",
                RequestContact = reqContact,
                RequestLocation = reqLocation,
                Color = color,
                Type = type ?? KeyboardButtonActionType.Text,
                Payload = command != null ? new PayloadBuilder(command,arguments).BuildToModel(): null,
                Hash = btnHash ?? null,
                AppID = btnAppId ?? 0,
                OwnerID = btnOwnerId ?? 0
            });
            return this;
        }
        
        public void AddButton(INucleusKeyboardButton button)
        {
            try
            {
                _bot.AliasesCommand.Add(button.Caption, button.Payload);
            }
            catch (Exception)
            {
                //Console.WriteLine($"Алиас {button.Caption} уже существует.");
            }
            _currentLine.Add(button);
        }

        public KeyboardBuilder AddLine()
        {
            _fullKeyboard.Add(_currentLine);
            _currentLine = new List<INucleusKeyboardButton>();
            return this;
        }

        public KeyboardBuilder SetOneTime()
        {
            _oneTimeKb = true;
            return this;
        }

        public KeyboardBuilder SetResize()
        {
            _resizeKb = true;
            return this;
        }

        public KeyboardBuilder SetSelective()
        {
            _isSelective = true;
            return this;
        }

        public NucleusKeyboard Build()
        {
            _fullKeyboard.Add(_currentLine);
            List<INucleusKeyboardButton> lineRemove = null;

            foreach(var line in _fullKeyboard)
            {
                if (line.Count == 0) lineRemove = line;
            }

            if(lineRemove != null) _fullKeyboard.Remove(lineRemove);
            return new NucleusKeyboard()
            {
                Buttons = _fullKeyboard,
                OneTimeKeyboard = _oneTimeKb,
                Selective = _isSelective,
                ResizeKeyboard = _resizeKb
            };
        }
    }
}