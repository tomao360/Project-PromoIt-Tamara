using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItProject.Model
{
    public class ActivistReport: BaseModel
    {
        public int ActivistID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int TotalMoney { get; set; }
        public int TotalCampaigns { get; set; }
    }
}
