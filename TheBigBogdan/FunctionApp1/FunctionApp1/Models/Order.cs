using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionApp1.Models
{
    class Order
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("ProductsName")]
        public string ProductsName { get; set; }
        [BsonElement("Coordinates")]
        public List<float> Coordinates { get; set; }
        [BsonElement("DriversCoordinates")]
        public List<List<float>> DriversCoordinates { get; set; }
        public Order(string productsname, List<float> coordinates)
        {
            ProductsName = productsname;
            Coordinates = coordinates;
        }
        public Order(string productsname, List<float> coordinates, List<float> driverscoordinates)
        {
            DriversCoordinates.Add(driverscoordinates);
            ProductsName = productsname;
            Coordinates = coordinates;
        }
        public Order()
        {

        }
    }
}
