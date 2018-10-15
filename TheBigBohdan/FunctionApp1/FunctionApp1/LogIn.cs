
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
namespace FunctionApp1
{
    public static class LogIn
    {
        [FunctionName("LogIn")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            ChamomileContext dbcontext = new ChamomileContext();
            IMongoCollection<User> userCollection = dbcontext.database.GetCollection<User>("Chamomile");
            string email = req.Query["email"];
            string name = req.Query["name"];
            string password = req.Query["password"];
            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;
            password = password ?? data?.password;
            email = email ?? data?.email;
            User user = new User(email, name, password);
            userCollection.InsertOne(user);
            return User.FindUser(user, true) != "Quest"
                ? (ActionResult)new OkObjectResult(JsonConvert.SerializeObject("Ok"))
                : new BadRequestObjectResult(JsonConvert.SerializeObject("Bad"));
        }
    }

    
}
