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
            Write("ERROR", text);
            Console.ResetColor();
            //throw new NotImplementedException();
        }
        private void Write(string type, object text)
        {
            Console.WriteLine($"({DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}) [{type}]: {text}");
        }

        public void Info(object text)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Write("INFO", text);
            Console.ResetColor();
            //throw new NotImplementedException();
        }

        public void Trace(object text)
        {
            Write("TRACE", text);
        }

        public void War(object text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Write("WARNING",text);
            Console.ResetColor();
            //throw new NotImplementedException();
        }
    }
}
