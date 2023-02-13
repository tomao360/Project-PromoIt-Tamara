using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItProject.Entities.Commands
{
    public class ActiveCampaignsGetCampaignsCmd: BaseCommand, ICommand
    {
        public class ActiveCampaignsParam
        {
            public string IdNumber;
        }

        public object  Execute(object param)
        {
            ActiveCampaignsParam activeCampaignsParam = new ActiveCampaignsParam();

            // Retrieve all active campaigns by Activist ID
            return System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.activeCampaigns.GetActiveCampaignsByActivistID(int.Parse(activeCampaignsParam.IdNumber)));
        }
    }
}
