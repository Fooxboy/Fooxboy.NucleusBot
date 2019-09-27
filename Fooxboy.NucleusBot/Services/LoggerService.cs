using Fooxboy.NucleusBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Fooxboy.NucleusBot.Services
{
    public class LoggerService : ILoggerService
    {
        public void Error(object text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Write(text);
            Console.ResetColor();
            //throw new NotImplementedException();
        }
        private void Write(object text)
        {
            Console.WriteLine(text);
        }

        public void Info(object text)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Write(text);
            Console.ResetColor();
            //throw new NotImplementedException();
        }

        public void Trace(object text)
        {
            Write(text);
        }

        public void War(object text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Write(text);
            Console.ResetColor();
            //throw new NotImplementedException();
        }
    }
}
