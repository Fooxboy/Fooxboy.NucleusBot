using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;

namespace Fooxboy.NucleusBot.SampleBot.Commands
{
    public class SampleGreenButtonCommand : INucleusCommand
    {
        public string Command => "sampleGreenButtonCommand";
        public string[] Aliases => new[] {"Зеленая вроде кнопка"};
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            sender.Text("Ты нажал на зеленую кнопку!", msg.ChatId);
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            logger.Trace("Команда sampleGreenButtonCommand инициализирована");
        }
    }
}