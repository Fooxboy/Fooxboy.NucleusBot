using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.NucleusBot.Models
{
    public class RootLongPollModel
    {
        [JsonProperty("ts")]
        public long Ts { get; set; }
        [JsonProperty("updates")]
        public List<UpdateLongPoll> Updates { get; set; }

        public class UpdateLongPoll
        {
            [JsonProperty("group_id")]
            public int GroupId { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("event_id")]
            public string EventId { get; set; }

            [JsonProperty("v")]
            public string V { get; set; }

            [JsonProperty("object")]
            public Object Object { get; set; }
        }
    }
}
