using Fooxboy.NucleusBot.Interfaces;

namespace Fooxboy.NucleusBot.Models
{
    public class NucleusKeyboardButton : INucleusKeyboardButton
    {
        public string Caption { get; set; }
        public bool RequestContact { get; set; }
        public bool RequestLocation { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }
        public string Payload { get; set; }
        public string Hash { get; set; }
        public string AppID { get; set; }
        public string OwnerID { get; set; }

        public NucleusKeyboardButton()
        {
            //Саня отдай сотку
        }
    }
}