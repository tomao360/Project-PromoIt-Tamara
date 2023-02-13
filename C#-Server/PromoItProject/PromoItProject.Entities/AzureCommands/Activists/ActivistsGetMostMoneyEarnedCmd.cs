using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.Activists
{
    public class ActivistsGetMostMoneyEarnedCmd: BaseCommand, ICommand
    {
        public ActivistsGetMostMoneyEarnedCmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            try
            {
                Log.LogEvent($"Start retrieving all the Social Activists (by earned money) from DB (Execute function in ActivistsGetMostMoneyEarnedCmd class)");
                // Retrieve the activists who earned the most money
                string json = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.activists.GetMostMoneyEarned());

                Log.LogEvent("All Social Activists (by earned money) were received from DB");
                return json;
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
        }
    }
}
