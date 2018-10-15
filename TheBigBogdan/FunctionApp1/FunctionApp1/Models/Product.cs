using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using MongoDB.Driver;
using MongoDB.Driver.Core;
namespace FunctionApp1
{
    public class Product
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Price")]
        public string Price { get; set; }
        
        [BsonElement("Products")]
        public List<string> Products { get; set; }
        [BsonElement("Amounts")]
        public List<string> Amounts { get; set; }
        [BsonElement("Time")]
        public string Time { get; set; }
        public Product(string name, string price, List<string> products, List<string> amounts, string time)
        {
            
            Name = name;
            Price = price;
            Products = products;
            Amounts = amounts;
            Time = time;
        }
        public Product(ObjectId id, string name, string price, List<string> products, List<string> amounts, string time)
        {
            
            Name = name;
            Price = price;
            Products = products;
            Amounts = amounts;
            Id = id;
            Time = time;
        }
        public Product()
        {

        }
        public static Product FindProduct(string product)
        {
            ChamomileContext dbcontext = new ChamomileContext();
            IMongoCollection<Product> productCollection = dbcontext.database.GetCollection<Product>("Product");
            Product reproduct = null;
            var filter = new BsonDocument("Name", product);
            var list = productCollection.Find(filter).ToList();
            foreach (var doc in list)
            {
                reproduct = doc;
            }
            return reproduct;
        }
    }
}
