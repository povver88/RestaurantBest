using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;
namespace FunctionApp1
{
    public static class SignIn
    {
        [FunctionName("SignIn")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            ChamomileContext dbcontext = new ChamomileContext();
            IMongoCollection<User> userCollection = dbcontext.database.GetCollection<User>("Chamomile");
            string email = req.Query["email"];
            
            string password = req.Query["password"];
            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            
            password = password ?? data?.password;
            email = email ?? data?.email;
            User user = new User(email, password);
            string username = User.FindUser(user, true);
            return (username != "Quest")
                ? (ActionResult)new OkObjectResult(user)
                : new BadRequestObjectResult("Wrong");
        }
        
    }
}
