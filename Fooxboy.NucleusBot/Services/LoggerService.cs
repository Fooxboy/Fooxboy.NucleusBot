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
        }
        private void Write(string type, object text)
        {
            Console.WriteLine($"({DateTime.Now.ToString("HH:mm:ss")}) [{type}]: {text}");
        }

        public void Info(object text)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Write("INFO", text);
            Console.ResetColor();
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
        }
    }
}
