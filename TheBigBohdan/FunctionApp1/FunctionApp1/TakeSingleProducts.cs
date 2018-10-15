
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Net.Http.Formatting;

namespace FunctionApp1
{
    public static class TakeSingleProducts
    {
        [FunctionName("TakeSingleProducts")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "TakeSingleProducts")]HttpRequest req, TraceWriter log)
        {
            ChamomileContext dbcontext = new ChamomileContext();
            IMongoCollection<SingleProduct> productCollection = dbcontext.database.GetCollection<SingleProduct>("SingleProducts");
            var products = productCollection.Find(Builders<SingleProduct>.Filter.Empty).ToList();
            string jsonString = JsonConvert.SerializeObject(products, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented });

            return products != null
                ? (ActionResult)new OkObjectResult(jsonString)
                : new BadRequestObjectResult(JsonConvert.SerializeObject("Bad"));
        }
    }
}
