using System.Collections.Generic;
using Fooxboy.NucleusBot.Interfaces;

namespace Fooxboy.NucleusBot.Models
{
    public class NucleusKeyboard : INucleusKeyboard
    {
        public List<List<INucleusKeyboardButton>> Buttons { get; set; }
        public bool OneTimeKeyboard { get; set; }
        public bool ResizeKeyboard { get; set; }
        public bool Selective { get; set; }

        public NucleusKeyboard()
        {
        }

    }
}