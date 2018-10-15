
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace FunctionApp1
{
    public static class UpdateProduct
    {
        [FunctionName("UpdateProduct")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdateProduct/{name}")]HttpRequest req,string name, TraceWriter log)
        {
            ChamomileContext dbcontext = new ChamomileContext();
            IMongoCollection<Product> productCollection = dbcontext.database.GetCollection<Product>("Product");
            string uname = req.Query["Name"];
            string uprice = req.Query["Price"];
            
            string utime = req.Query["Time"];
            List<string> uproducts = new List<string>();
            List<string> uamounts = new List<string>();

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            var data = JsonConvert.DeserializeObject<Product>(requestBody);

            uname = data.Name;
            uprice = data.Price;
            
            uproducts = data.Products;
            uamounts = data.Amounts;
            utime = data.Time;
            var object1 = productCollection.Find(Builders<Product>.Filter.Eq("Name", name)).ToList();
            ObjectId id = new ObjectId();
            foreach (var doc in object1)
            {
                id = doc.Id;
            }
            Product product = new Product(id, uname, uprice, uproducts, uamounts, utime);
            productCollection.ReplaceOne(Builders<Product>.Filter.Eq("Name", name), product);

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
