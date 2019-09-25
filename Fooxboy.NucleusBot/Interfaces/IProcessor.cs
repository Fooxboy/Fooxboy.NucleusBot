using Fooxboy.NucleusBot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.NucleusBot.Interfaces
{
    public interface IProcessor
    {
        void Start(Message message);
    }
}
