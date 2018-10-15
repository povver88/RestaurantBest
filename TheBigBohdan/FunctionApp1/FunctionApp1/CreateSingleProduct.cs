using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;

namespace FunctionApp1
{
    public static class CreateSingleProduct
    {
        static bool result = false;
        [FunctionName("CreateSingleProduct")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "CreateSingleProduct")]HttpRequest req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            ChamomileContext dbcontext = new ChamomileContext();
            IMongoCollection<SingleProduct> productCollection = dbcontext.database.GetCollection<SingleProduct>("SingleProducts");
            string path = @"C:\Users\Dell\Source\Repos\FunctionApp1\FunctionApp1\Menu\Menu1.txt";
            
            string name = "";
            string amount = "";
            SingleProduct singleProduct = new SingleProduct(name, amount);
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string line = "";
                    int j = 0;
                    for (int i = 0; (line = sr.ReadLine()) != "*"; i++)
                    {
                        if (i%2==0)
                        {
                            name = line;
                        }
                        else if (i%2!=0)
                        {
                            amount = line;
                            singleProduct = new SingleProduct(name, amount);
                            productCollection.InsertOne(singleProduct);
                        }
                    }
                }
                result = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            
            
                return result != false
                    ? (ActionResult)new OkObjectResult(JsonConvert.SerializeObject("Ok"))
                    : new BadRequestObjectResult(JsonConvert.SerializeObject("Bad"));
        }
    }
}
