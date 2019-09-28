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
        private bool _oneTimeKb = false;
        private bool _resizeKb = false;
        private bool _isSelective = false;

        public KeyboardBuilder AddButton(string text, string command, string arguments = null, bool reqContact= false, bool reqLocation = false, KeyboardButtonColor color = default, KeyboardButtonActionType type = null,
            string btnPayload = null, string btnHash = null, ulong? btnAppId = null, ulong? btnOwnerId = null)
        {
            AddButton(new NucleusKeyboardButton()
            {
                Caption = text ?? "",
                RequestContact = reqContact,
                RequestLocation = reqLocation,
                Color = color,
                Type = type ?? KeyboardButtonActionType.Text,
                Payload = btnPayload ?? "",
                Hash = btnHash ?? "",
                AppID = btnAppId ?? 0,
                OwnerID = btnOwnerId ?? 0
            });
            return this;
        }
        
        public void AddButton(INucleusKeyboardButton button)
        {
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