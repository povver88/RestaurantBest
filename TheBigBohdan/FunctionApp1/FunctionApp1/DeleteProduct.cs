
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using MongoDB.Driver;
using MongoDB.Bson;

namespace FunctionApp1
{
    public static class DeleteProduct
    {
        [FunctionName("DeleteProduct")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteProduct/{name}")]HttpRequest req,string name, TraceWriter log)
        {
            ChamomileContext dbcontext = new ChamomileContext();
            IMongoCollection<Product> productCollection = dbcontext.database.GetCollection<Product>("Product");
            
            productCollection.DeleteOne(Builders<Product>.Filter.Eq("Name", name));
            

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
