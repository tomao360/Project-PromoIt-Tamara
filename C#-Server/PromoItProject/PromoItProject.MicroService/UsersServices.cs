using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PromoItProject.Entities;

namespace PromoItProject.MicroService
{
    public static class UsersServices
    {
        //http://localhost:7243/api/Users/Get
        //http://localhost:7243/api/Users/Get/id
        //http://localhost:7243/api/Users/Add
        //http://localhost:7243/api/Users/Update/id
        //http://localhost:7243/api/Users/Remove/id
        [FunctionName("Users")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Users/{action}/{param?}")] HttpRequest req, string action, string param)
        {
            string cmdName = "Users." + action;
            try
            {
                ICommand command = MainManager.Instance.commandsManager.CommandList[cmdName];
                if (command != null)
                {
                    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    var result = command.Execute(param, requestBody);
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

