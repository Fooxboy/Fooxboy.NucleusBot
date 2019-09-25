using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.NucleusBot.Interfaces
{
    public interface IMessageSenderService
    {
        void Text(string text, long to, object keyboard = null, long from = 0);
        void Image(string to, string text = null, object image = null, long from = 0);
    }
}
