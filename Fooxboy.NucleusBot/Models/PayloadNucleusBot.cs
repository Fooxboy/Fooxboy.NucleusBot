using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.NucleusBot.Models
{
    public class PayloadNucleusBot
    {
        [JsonProperty("args")]
        public List<string> Arguments { get; set; }
        [JsonProperty("command")]
        public string Command { get; set; }
    }
}
