using Fooxboy.NucleusBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.NucleusBot.Models
{
    public class UnknownCommand : INucleusCommand
    {
        public string Command => "unknown";

        public string[] Aliases => null;

        public void Execute(Message msg, IMessageSenderService sender, IBot bot)
        {
            sender.Text("Упс. Я не знаю такой команды :(", msg.Platform == Enums.MessengerPlatform.Vkontakte? msg.MessageVK.PeerId.Value: msg.MessageTG.Chat.Id);
        }

        public void Init(IBot bot, ILoggerService logger)
        {
            logger.War("Вы не передали реализацию неизвестной команды, по этому используется встроенная. Передайте экзепмляр своей команды в конструкторе Bot.");
            //throw new NotImplementedException();
        }
    }
}
