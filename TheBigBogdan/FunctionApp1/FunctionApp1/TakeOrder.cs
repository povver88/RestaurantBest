
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using MongoDB.Driver;
using FunctionApp1.Models;
using System.Collections.Generic;

namespace FunctionApp1
{
    public static class TakeOrder
    {
        [FunctionName("TakeOrder")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "TakeOrder")]HttpRequest req, TraceWriter log)
        {
            ChamomileContext dbcontext = new ChamomileContext();
            IMongoCollection<Order> ordersCollection = dbcontext.database.GetCollection<Order>("Orders");
            List<Order> orders = ordersCollection.Find(Builders<Order>.Filter.Empty).ToList();
            
            return orders != null
                ? (ActionResult)new OkObjectResult(JsonConvert.SerializeObject(orders))
                : new BadRequestObjectResult(JsonConvert.SerializeObject("Bad"));
        }
    }
}
