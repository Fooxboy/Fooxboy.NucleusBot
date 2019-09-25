using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.NucleusBot.Models
{
    public delegate void NewMessageDelegate(Message message);
    public delegate void UserMessageAction(UserMessagesActionModel info);
}
