﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.CommunicationProviders
{
    public class BaseCommunicationProvider
    {
        public BaseCommunicationProvider(Logger log)
        {
            Log = log;
        }

        public Logger Log { get; set; }
    }
}
