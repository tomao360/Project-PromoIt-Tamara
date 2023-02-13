using PromoItProject.Entities.AzureCommands.ActiveCampaigns;
using PromoItProject.Entities.AzureCommands.Activists;
using PromoItProject.Entities.AzureCommands.Auth0;
using PromoItProject.Entities.AzureCommands.BusinessComapnies;
using PromoItProject.Entities.AzureCommands.Campaigns;
using PromoItProject.Entities.AzureCommands.Messages;
using PromoItProject.Entities.AzureCommands.Organizations;
using PromoItProject.Entities.AzureCommands.Products;
using PromoItProject.Entities.AzureCommands.Twitter;
using PromoItProject.Entities.AzureCommands.Users;
using PromoItProject.Entities.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities
{
    public class CommandsManager: BaseCommand
    {
        public CommandsManager(Logger log) : base(log) { }

        private Dictionary<string, ICommand> _commandList;
        public Dictionary<string, ICommand> CommandList
        {
            get
            {
                if (_commandList == null) Init();
                return _commandList;
            }
        }

        private void Init()
        {
            _commandList = new Dictionary<string, ICommand>
            {
                // Build the dictionary

                // Active Campaigns
                { "ActiveCampaigns.Get", new ActiveCampaignsGetCmd(base.Log) },
                { "ActiveCampaigns.GetCampaigns", new ActiveCampaignsGetCampaignsCmd(base.Log) },
                { "ActiveCampaigns.Add", new ActiveCampaignsAddCmd(base.Log) },
                { "ActiveCampaigns.UpdateAddMoney", new ActiveCampaignsUpdateAddMoneyCmd(base.Log) },
                { "ActiveCampaigns.UpdateSubtractMoney", new ActiveCampaignsUpdateSubtractMoney(base.Log) },

                // Social Activists
                { "Activists.Get", new ActivistsGetCmd(base.Log) },
                { "Activists.GetMostMoneyEarned", new ActivistsGetMostMoneyEarnedCmd(base.Log) },
                { "Activists.GetMostPromotedCampaigns", new ActivistsGetMostPromotedCampaignsCmd(base.Log) },
                { "Activists.Add", new ActivistsAddCmd(base.Log) },
                { "Activists.Update", new ActivistUpdateCmd(base.Log) },
                { "Activists.Remove", new ActivistsRemoveCmd(base.Log) },

                // Roles
                { "roles", new Auth0Cmd(base.Log) },

                // Campaigns
                { "Campaigns.Get", new CampaignsGetCmd(base.Log) },
                { "Campaigns.GetCampaignsAndORG", new CampaignsGetCampaignsAndORGCmd(base.Log) },
                { "Campaigns.GetPopularCampaign", new CampaignsGetPopularCampaignCmd(base.Log) },
                { "Campaigns.GetProfitableCampaign", new CampaignsGetProfitableCampaignCmd(base.Log) },
                { "Campaigns.Add", new CampaignsAddCmd(base.Log) },
                { "Campaigns.Update", new CampaignsUpdateCmd(base.Log) },
                { "Campaigns.Remove", new CampaignsRemoveCmd(base.Log) },

                // Business Companies
                { "Companies.Get", new CompaniesGetCmd(base.Log) },
                { "Companies.GetShipment", new CompaniesGetShipmentCmd(base.Log) },
                { "Companies.GetShipmentByID", new CompaniesGetShipmentByIDCmd(base.Log) },
                { "Companies.GetCountOfDonations", new CompaniesGetCountOfDonationsCmd(base.Log) },
                { "Companies.Add", new CompaniesAddCmd(base.Log) },
                { "Companies.Update", new CompaniesUpdateCmd(base.Log) },
                { "Companies.Remove", new CompaniesRemoveCmd(base.Log) },

                // Contact Messages
                { "Messages.Get", new MessagesGetCmd(base.Log) },
                { "Messages.Add", new MessagesAddCmd(base.Log) },
                { "Messages.Remove", new MessagesRemoveCmd(base.Log) },

                // Non-Profit Organizations
                { "Organizations.Get", new OrganizationsGetCmd(base.Log) },
                { "Organizations.GetCampaignsProducts", new OrganizationsGetCampaignsProductsCmd(base.Log) },
                { "Organizations.Add", new OrganizationsAddCmd(base.Log) },
                { "Organizations.Update", new OrganizationsUpdateCmd(base.Log) },
                { "Organizations.Remove", new OrganizationsRemoveCmd(base.Log) },

                // Donated Products
                { "Products.Get", new ProductsGetCmd(base.Log) },
                { "Products.Add", new ProductsAddCmd(base.Log) },
                { "Products.UpdateBought", new ProductsUpdateBoughtCmd(base.Log) },
                { "Products.UpdateShipped", new ProductsUpdateShippedCmd(base.Log) },

                // Users
                { "Users.Get", new UsersGetCmd(base.Log) },
                { "Users.Add", new UsersAddCmd(base.Log) },

                // Twitter
                { "Twitter.Add", new TwitterAddCmd(base.Log) }
            };
        }
    }
}
