using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItProject.Model
{
    public class ActiveCampaign: BaseModel
    {
        public int ActiveCampID { get; set; }
        public int ActivistID { get; set;}
        public int CampaignID { get; set;}
        public string TwitterUserName { get; set;}
        public string Hashtag { get; set;}
        public int MoneyEarned { get; set;}
        public string CampaignName { get; set;}
        public int TweetsNumber { get; set; }
    }
}
