using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities
{
    public class BasePromotionSystem
    {
        public BasePromotionSystem(Logger log)
        {
            Log = log;
        }

        public Logger Log { get; set; }
    }
}
