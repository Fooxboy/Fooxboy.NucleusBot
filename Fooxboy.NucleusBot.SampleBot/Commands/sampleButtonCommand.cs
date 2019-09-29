using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;

namespace Fooxboy.NucleusBot.SampleBot.Commands
{
    public class sampleButtonCommand : INucleusCommand
    {
        public string Command => "sampleButtonCommand";
        public string[] Aliases => new[] {"Кнопка"};
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            sender.Text("Ты нажал на кнопку!", msg.ChatId);
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            logger.Trace("Команда sampleButtonCommand инициализирована");
        }
    }
}