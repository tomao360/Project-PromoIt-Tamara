using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItProject.Model
{
    public class ProductCampaignORG: BaseModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int BusinessID { get; set; }
        public int CampaignID { get; set; }
        public string Bought { get; set; }
        public string Shipped { get; set; }
        public string CampaignName { get; set; }
        public string Hashtag { get; set; }
        public string OrganizationName { get; set; }
        public int OrganizationID { get; set; }
    }
}
