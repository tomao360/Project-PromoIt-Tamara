 using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PromoItProject.Entities;

namespace PromoItProject.MicroService
{
    public static class TwitterServices
    {
        //http://localhost:7243/api/Twitter/Get/userName/hashtag
        //http://localhost:7243/api/Twitter/Add
        [FunctionName("Twitter")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Twitter/{action}/{userName?}/{hashtag?}")] HttpRequest req, string action, string userName, string hashtag)
        {
            string cmdName = "Twitter." + action;
            try
            {
                ICommand command = MainManager.Instance.commandsManager.CommandList[cmdName];
                if (command != null)
                {
                    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    var result = command.Execute(userName, hashtag, requestBody);
                    if (result != null)
                    {
                        return new OkObjectResult(result);
                    }
                    else
                    {
                        MainManager.Instance.logger.LogError("An error occurred while executing the request");
                        return new BadRequestObjectResult("An error occurred while executing the request. Please try again in a few minutes, if the problem continues contact ProLobby.");
                    }
                }
                else
                {
                    MainManager.Instance.logger.LogError("Command not found");
                    return new NotFoundObjectResult("Command not found");
                }
            }
            catch (Exception ex)
            {
                MainManager.Instance.logger.LogException(ex.Message, ex);
                return new BadRequestObjectResult("An error occurred while executing the request. Please try again in a few minutes, if the problem continues contact ProLobby.");
            }

        }
    }
}
