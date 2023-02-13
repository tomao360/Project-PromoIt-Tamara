using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItProject.Model
{
    public class User: BaseModel
    {
        public int UserID { get; set; }
        public string Email { get; set; }
    }
}
