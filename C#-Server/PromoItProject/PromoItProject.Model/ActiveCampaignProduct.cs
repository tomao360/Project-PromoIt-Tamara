using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItProject.Model
{
    public class ActiveCampaignProduct: BaseModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int BusinessID { get; set; }
        public int CampaignID { get; set; }
        public string Bought { get; set; }
        public string Shipped { get; set; }
        public int ActiveCampID { get; set; }
        public int ActivistID { get; set; }
        public string TwitterUserName { get; set; }
        public string Hashtag { get; set; }
        public int MoneyEarned { get; set; }
        public string CampaignName { get; set; }
        public int TweetsNumber { get; set; }
    }
}
