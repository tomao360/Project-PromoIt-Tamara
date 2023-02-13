using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities
{
    public class MainManager
    {
        private MainManager()
        {
            Init();
            logger.LogEvent("Program Started");
        }

        private static readonly MainManager _instance = new MainManager();
        public static MainManager Instance { get { return _instance; } }

        public void Init()
        {
            string logTypeString = Environment.GetEnvironmentVariable("LogType");
            Logger.LogType logType;

            if (logTypeString == "File")
            {
                logType = Logger.LogType.File;
            }
            else if (logTypeString == "DB")
            {
                logType = Logger.LogType.DB;
            }
            else if (logTypeString == "Console")
            {
                logType = Logger.LogType.Console;
            }
            else
            {
                logType = Logger.LogType.None;
            }
            // Creating instance of the Logger
            logger = new Logger(logType);

            // Creating instances of all the entities
            commandsManager = new CommandsManager(logger);
            users = new Users(logger);
            nonProfitOrganizations = new NonProfitOrganizations(logger);
            campaigns = new Campaigns(logger);
            businessCompanies = new BusinessCompanies(logger);
            donatedProducts = new DonatedProducts(logger);
            activists = new Activists(logger);
            activeCampaigns = new ActiveCampaigns(logger);
            ContactUsMessages = new ContactUsMessages(logger);

            // Creating instance of the Twitter
            twitter = new PromoItProject.CommunicationProviders.Twitter(logger);
        }

        public Logger logger;
        public CommandsManager commandsManager;
        public Users users;
        public NonProfitOrganizations nonProfitOrganizations;
        public Campaigns campaigns;
        public BusinessCompanies businessCompanies;
        public DonatedProducts donatedProducts;
        public Activists activists;
        public ActiveCampaigns activeCampaigns;
        public ContactUsMessages ContactUsMessages;
        public PromoItProject.CommunicationProviders.Twitter twitter;
    }
}
