using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItProject.Model
{
    public class OrganizationReport: BaseModel
    {
        public int OrganizationID { get; set; }
        public string OrganizationName { get; set; }
        public string Email { get; set; }
        public string LinkToWebsite { get; set; }
        public string Description { get; set; }
        public int CampaignID { get; set; }
        public string CampaignName { get; set; }
        public string LinkToLandingPage { get; set; }
        public string Hashtag { get; set; }
        public int TotalActivists { get; set; }
        public int TotalTweets { get; set;}
        public int TotalProducts { get; set; }
        public int TotalMoney { get; set; }
        public int TotalCampaigns { get; set; }

    }
}
