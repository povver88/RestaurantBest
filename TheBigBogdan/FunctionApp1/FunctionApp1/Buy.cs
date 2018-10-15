
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
using FunctionApp1.Models;

namespace FunctionApp1
{
    public static class Buy
    {
        [FunctionName("Buy")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Buy/{name}/{coordinates}/{amount}")]HttpRequest req, string name, List<float> coordinates, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            ChamomileContext dbcontext = new ChamomileContext();
            IMongoCollection<Product> productCollection = dbcontext.database.GetCollection<Product>("Product");
            IMongoCollection<SingleProduct> singleProductCollection = dbcontext.database.GetCollection<SingleProduct>("SingleProducts");
            IMongoCollection<Order> ordersCollection = dbcontext.database.GetCollection<Order>("Orders");

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            var data = JsonConvert.DeserializeObject(requestBody);

            Product product = new Product();
            var object2 = productCollection.Find(Builders<Product>.Filter.Eq("Name", name)).ToList();
            foreach (var doc in object2)
            {
                
                product = doc;
            }
            List<string> products = product.Products;
            List<string> amounts = product.Amounts;
            SingleProduct singleProduct = new SingleProduct();
            SingleProduct firstProduct = new SingleProduct();
            ObjectId id = new ObjectId();
            for (int i = 0; i < products.Count; i++)
            {
                var object1 = singleProductCollection.Find(Builders<SingleProduct>.Filter.Eq("Name", products[i])).ToList();

                foreach (var doc in object1)
                {
                    id = doc.Id;
                    firstProduct = doc;
                }
                SingleProduct reproduct = new SingleProduct();
                singleProduct = new SingleProduct(id, products[i], System.Convert.ToString(System.Convert.ToInt32(firstProduct.Amount) - System.Convert.ToInt32(amounts[i])));

                singleProductCollection.ReplaceOne(Builders<SingleProduct>.Filter.Eq("Name", products[i]), singleProduct);
                var list = singleProductCollection.Find(new BsonDocument("Amount", singleProduct.Amount)).ToList();
                foreach (var doc in list)
                {
                    reproduct = doc;
                }
                if (reproduct.Amount == "0")
                {
                    singleProductCollection.DeleteOne(Builders<SingleProduct>.Filter.Eq("Name", reproduct.Name));
                }
            }
            Order order = new Order(product.Name, coordinates);
            ordersCollection.InsertOne(order);
            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
        
    }
}
