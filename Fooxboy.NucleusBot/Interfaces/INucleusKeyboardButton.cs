namespace Fooxboy.NucleusBot.Interfaces
{
    public interface INucleusKeyboardButton
    {
        //Общие свойства
        string Caption { get; set; }
        
        //Только Телеграм
        bool RequestContact { get; set; }
        bool RequestLocation { get; set; }
        
        //Только ВК
        string Color { get; set; }
        string Type { get; set; }
        string Payload { get; set; }
        string Hash { get; set; }
        string AppID { get; set; }
        string OwnerID { get; set; }
    }
}