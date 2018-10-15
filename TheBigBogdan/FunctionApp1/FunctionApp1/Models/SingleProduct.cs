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
    class SingleProduct
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Amount")]
        public string Amount { get; set; }
        public SingleProduct(string name, string amount)
        {
            Name = name;
            Amount = amount;
            
        }
        public SingleProduct(ObjectId id,string name, string amount)
        {
            Name = name;
            Amount = amount;
            Id = id;
        }
        public SingleProduct()
        {

        }

    }
}
