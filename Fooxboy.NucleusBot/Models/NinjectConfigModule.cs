using Fooxboy.NucleusBot.Interfaces;
using Fooxboy.NucleusBot.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.NucleusBot.Models
{
    public class NinjectConfigModule : NinjectModule
    {
        public override void Load()
        {
            //Bind<IBot>().To<MessageSenderService>();
        }
    }
}
