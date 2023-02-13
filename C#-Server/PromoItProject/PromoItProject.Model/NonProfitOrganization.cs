using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItProject.Model
{
    public class NonProfitOrganization: BaseModel
    {
        public int OrganizationID { get; set; }
        public string OrganizationName { get; set; }
        public string Email { get; set;}
        public string LinkToWebsite { get; set; }
        public string Description { get; set; }
        public string DeleteAnswer { get; set; }
    }
}
