using Fooxboy.NucleusBot.Interfaces;
using VkNet.Enums.SafetyEnums;

namespace Fooxboy.NucleusBot.Models
{
    public class NucleusKeyboardButton : INucleusKeyboardButton
    {
        public string Caption { get; set; }
        public bool RequestContact { get; set; }
        public bool RequestLocation { get; set; }
        public PayloadNucleusBot Payload { get; set; }
        public string Hash { get; set; }
        public ulong AppID { get; set; }
        public ulong OwnerID { get; set; }
        public KeyboardButtonColor Color { get; set; }
        public KeyboardButtonActionType Type { get; set; }

    }
}