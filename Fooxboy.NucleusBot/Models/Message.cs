using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.NucleusBot.Models
{
    public class Message: VkNet.Model.Message
    {

        public PayloadNucleusBot PayloadNucleusBot { get; set; }
    }
}
