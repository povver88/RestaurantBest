
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FunctionApp1
{
    public static class CreateProduct
    {
        [FunctionName("CreateProduct")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            ChamomileContext dbcontext = new ChamomileContext();
            IMongoCollection<Product> productCollection = dbcontext.database.GetCollection<Product>("Product");
            IMongoCollection<SingleProduct> singleProductCollection = dbcontext.database.GetCollection<SingleProduct>("SingleProducts");
            string name = req.Query["Name"];
            string price = req.Query["Price"];
            
            string time = req.Query["Time"];
            List<string> products = new List<string>();
            List<string> amounts = new List<string>();
            
            string requestBody = new StreamReader(req.Body).ReadToEnd();
            var data = JsonConvert.DeserializeObject<Product>(requestBody);
            
            name = data.Name;
            price = data.Price;
            
            products = data.Products;
            amounts = data.Amounts;
            time = data.Time;
            Product product = new Product(name, price, products, amounts, time);

            
           
            productCollection.InsertOne(product);
            return name != null
                ? (ActionResult)new OkObjectResult(JsonConvert.SerializeObject("Ok"))
                : new BadRequestObjectResult(JsonConvert.SerializeObject("Bad"));
        }
    }
}
