
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System.Collections.Generic;
using MongoDB.Driver;
using FunctionApp1.Models;
using System;

namespace FunctionApp1
{
    public static class TakeOneOrder
    {
        [FunctionName("TakeOneOrder")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "TakeOneOrder/{name}/{driverscoordinates}")]HttpRequest req,string name,List<float> driverscoordinates, TraceWriter log)
        {
            ChamomileContext dbcontext = new ChamomileContext();
            IMongoCollection<Order> ordersCollection = dbcontext.database.GetCollection<Order>("Orders");
            Order order = new Order();
            var object1 = ordersCollection.Find(Builders<Order>.Filter.Eq("Name", name)).ToList();
            foreach(var doc in object1)
            {
                order = new Order(doc.ProductsName, doc.Coordinates, driverscoordinates);
            }
            if(order.DriversCoordinates.Count >= 3)
            {

            }
            ordersCollection.ReplaceOne(Builders<Order>.Filter.Eq("Name", name), order);
            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
        public static float FindWay(List<float> a, List<float> b)
        {
            float ab = (float)Math.Sqrt((((b[0] - a[0]) * (b[0] - a[0])) + ((b[1] - a[1]) * (b[1] - a[1]))));
            return ab;
        }
    }
}
