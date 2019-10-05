using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using Fooxboy.NucleusBot.SampleBot.Commands;
using Fooxboy.NucleusBot.SampleBot.Services;
using System;

namespace Fooxboy.NucleusBot.SampleBot
{
    class Program
    {
        const string vkTokenGroup = "937f504522a1e8a238c7feee3258ec1f875ebe0053eb927f65aff9a76de0e0a6e8795f7f249f1a6fb8292";
        const string tgTokenBot = "919369078:AAE3lsyMaA0kNMste8Qx9IxGkN6aiwgBSDM";
        const long groupVkId = 182424549;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //Создание параметров для бота.
            var settings = new BotSettings()
            {
                GroupId = groupVkId,
                Messenger = Enums.MessengerPlatform.VkontakteAndTelegram,
                TGToken = tgTokenBot,
                VKToken = vkTokenGroup
            };
            //Создание экзмпляра класса бота.
            IBot bot = new NucleusBot.Bot(settings, new Commands.UnknownCommand());

            //Установка команд.
            bot.SetCommands(new HelloCommand(),
                new KbCommand(),
                new SampleButtonCommand(),
                new SampleGreenButtonCommand(),
                new SampleRedButtonCommand());

            //Установка сервисов.
            bot.SetServices(new CounterService());
            
            //Запуск бота
            bot.Start();

            //Чтобы консоль тупо не закрылась))
            Console.ReadLine();
        }
    }
}
