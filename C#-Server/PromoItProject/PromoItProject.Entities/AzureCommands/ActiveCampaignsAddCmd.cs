using PromoItProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItProject.Entities.Commands
{
    public class ActiveCampaignsAddCmd: BaseCommand, ICommand
    {
        public object Execute (object param)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            // Deserialize the request body into an ActiveCampaign object
            ActiveCampaign activeCampaign = System.Text.Json.JsonSerializer.Deserialize<ActiveCampaign>(requestBody);

            // Check if all required fields are present
            if (activeCampaign.ActivistID != null && activeCampaign.CampaignID != null && activeCampaign.TwitterUserName != "" && activeCampaign.Hashtag != "" && activeCampaign.CampaignName != "")
            {
                // Insert the active campaign into the DB
                MainManager.Instance.activeCampaigns.InsertActiveCampaignToDB(activeCampaign.ActivistID, activeCampaign.CampaignID, activeCampaign.TwitterUserName, activeCampaign.Hashtag, activeCampaign.CampaignName);

                return  "This POST request executed successfully";
            }

            // Return a failure message if the required fields are not present
            return "Failed POST Request";
        }
    }
}
