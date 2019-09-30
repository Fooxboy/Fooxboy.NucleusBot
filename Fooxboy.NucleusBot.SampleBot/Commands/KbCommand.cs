using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using VkNet.Enums.SafetyEnums;

namespace Fooxboy.NucleusBot.SampleBot.Commands
{
    public class KbCommand : INucleusCommand
    {
        public string Command => "kb";
        public string[] Aliases => new string[] {"keyboard"};
        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            var builder = new KeyboardBuilder(bot);
            builder.AddButton("Кнопка", "sampleButtonCommand", color:KeyboardButtonColor.Default);
            builder.AddLine();
            builder.AddButton("Зеленая кнопка", "loh", color:KeyboardButtonColor.Default);
            builder.AddButton("Красная кнопка", "boh", color:KeyboardButtonColor.Default);
            var kb = builder.Build();
            sender.Text("Вот тебе клавиатура, комрад", msg.ChatId, kb);
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            logger.Trace("Команда kb инициализирована");
        }
    }
}