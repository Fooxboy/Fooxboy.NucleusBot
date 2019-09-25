using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.NucleusBot
{
    public class Processor : IProcessor
    {
        ILoggerService _logger;
        IBot _bot;
        public Processor(ILoggerService logger, IBot bot)
        {
            _logger = logger;
            _bot = bot;
        }
        public void Start(Message message)
        {
            _logger.Trace("Начало обработки сообщения.");

            var command = string.Empty;
            if (message.Payload == null) command = message.Text.Split(' ')[0];
            else
            {
                message.PayloadNucleusBot = JsonConvert.DeserializeObject<PayloadNucleusBot>(message.Payload);
                command = message.PayloadNucleusBot.Command;
            }


        }
    }
}
