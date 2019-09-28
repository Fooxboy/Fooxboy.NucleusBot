using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using System;

namespace Fooxboy.NucleusBot.SampleBot
{
    class Program
    {
        const string vkTokenGroup = "";
        const string tgTokenBot = "";
        const long groupVkId = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var settings = new BotSettings()
            {
                GroupId = groupVkId,
                Messenger = Enums.MessengerPlatform.VkontakteAndTelegram,
                TGToken = tgTokenBot,
                VKToken = vkTokenGroup
            };
            IBot bot = new NucleusBot.Bot(settings, new UnknownCommand());
        }
    }
}
