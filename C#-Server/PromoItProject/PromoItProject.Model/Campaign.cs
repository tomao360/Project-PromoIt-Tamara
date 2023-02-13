using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItProject.Model
{
    public class Campaign: BaseModel
    {
        public int CampaignID { get; set; }
        public string CampaignName { get; set;}
        public string LinkToLandingPage { get; set;}
        public string Hashtag { get; set; }
        public int OrganizationID { get; set; }
        public string DeleteAnswer { get; set; }
    }
}
