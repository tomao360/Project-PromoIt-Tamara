using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItProject.Model
{
    public class DonatedProduct: BaseModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int BusinessID { get; set; }
        public int CampaignID { get; set; }
        public string Bought { get; set; }
        public string Shipped { get; set; }
    }
}
