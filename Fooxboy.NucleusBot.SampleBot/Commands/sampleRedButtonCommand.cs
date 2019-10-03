using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;

namespace Fooxboy.NucleusBot.SampleBot.Commands
{
    public class SampleRedButtonCommand : INucleusCommand
    {
        public string Command => "sampleRedButtonCommand";
        public string[] Aliases => new[] {"Красная но не кнопка"};
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            sender.Text("Ты нажал на красную кнопку!", msg.ChatId);
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            logger.Trace("Команда sampleRedButtonCommand инициализирована");
        }
    }
}