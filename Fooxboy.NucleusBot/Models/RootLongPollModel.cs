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
            [JsonProperty("type")]
            public string Type { get; set; }
            [JsonProperty("object")]
            public object @Object { get; set; }
            [JsonProperty("group_id")]
            public long GroupId { get; set; }
        }
    }
}
