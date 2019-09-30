using Fooxboy.NucleusBot.Models;

namespace Fooxboy.NucleusBot.Interfaces
{
    public interface INucleusKeyboardButton
    {
        /// <summary>
        /// Текст на кнопке
        /// </summary>
        string Caption { get; set; }
        
        //Только Телеграм
        bool RequestContact { get; set; }
        bool RequestLocation { get; set; }

        //Только ВК
        VkNet.Enums.SafetyEnums.KeyboardButtonColor Color { get; set; }
        VkNet.Enums.SafetyEnums.KeyboardButtonActionType Type { get; set; }
        PayloadNucleusBot Payload { get; set; }
        string Hash { get; set; }
        ulong AppID { get; set; }
        ulong OwnerID { get; set; }
    }
}