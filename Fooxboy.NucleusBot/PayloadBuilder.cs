using Fooxboy.NucleusBot.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.NucleusBot
{
    public class PayloadBuilder
    {
        PayloadNucleusBot _payload;

        public PayloadBuilder(string command = null, List<string> arguments = null)
        {
            if (command != null) _payload = new PayloadNucleusBot() { Command = command, Arguments = arguments };
            else _payload = new PayloadNucleusBot();
        }

        public string BuildToString() => JsonConvert.SerializeObject(_payload);
        public PayloadNucleusBot BuildToModel() => _payload;
        public void SetCommand(string command) => _payload.Command = command;
        public void SetArgumens(List<string> arguments) => _payload.Arguments = arguments;
        public PayloadNucleusBot BuildToModelFromString(string payload) => JsonConvert.DeserializeObject<PayloadNucleusBot>(payload);
        public string BuildToStringFromModel(PayloadNucleusBot payload) => JsonConvert.SerializeObject(payload);
    }
}
