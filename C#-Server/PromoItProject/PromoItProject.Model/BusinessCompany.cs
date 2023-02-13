using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItProject.Model
{
    public class BusinessCompany: BaseModel
    {
        public int BusinessID { get; set; }
        public string BusinessName { get; set; }
        public string Email { get; set; }
        public string DeleteAnswer { get; set; }
    }
}
