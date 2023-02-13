using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities.AzureCommands.Auth0
{
    public class Auth0Cmd: BaseCommand, ICommand
    {
        public Auth0Cmd(Logger log) : base(log) { }

        public object Execute(params object[] param)
        {
            if (param[0] != null)
            {
                try
                {
                    var urlGetRoles = $" https://dev-ti3u1n80psnj3o5q.us.auth0.com/api/v2/users/{param[0]}/roles";

                    var client = new RestClient(urlGetRoles);
                    var request = new RestRequest("", Method.Get);
                    request.AddHeader("authorization", Environment.GetEnvironmentVariable("BearerAuth0"));

                    var response = client.Execute(request);
                    if (response.IsSuccessful)
                    {
                        var json = JArray.Parse(response.Content);

                        Log.LogEvent("Got a User Role from Auth0");
                        return json;
                    }
                    else
                    {
                        Log.LogError($"A problem occurred while getting a User Role from Auth0");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Log.LogException(ex.Message, ex);
                    throw;
                }
            }
            else
            {
                Log.LogError("The parameter was not received from the client in the Execute function in Auth0Cmd class");
                return null;
            }
        }
    }
}
