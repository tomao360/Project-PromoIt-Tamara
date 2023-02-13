using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities
{
    public class BaseCommand: BasePromotionSystem
    {
        public BaseCommand(Logger log) : base(log) { }
    }
}
