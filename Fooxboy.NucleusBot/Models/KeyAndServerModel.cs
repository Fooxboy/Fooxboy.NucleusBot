using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.NucleusBot.Models
{
    public class KeyAndServerModel
    {
        public Response response { get; set; }
        public class Response
        {
            [JsonProperty("key")]
            public string Key { get; set; }
            [JsonProperty("server")]
            public string Server { get; set; }
            [JsonProperty("ts")]
            public long Ts { get; set; }
        }
    }

}
