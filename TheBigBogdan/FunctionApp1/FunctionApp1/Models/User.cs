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
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Password")]
        public string Password { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("Root")]
        public string Root { get; set; }
        public User(string email, string name, string password)
        {
            Email = email;
            Name = name;
            Password = password;
        }
        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }
        public static string FindUser(User user, bool s)
        {
            ChamomileContext dbcontext = new ChamomileContext();
            IMongoCollection<User> userCollection = dbcontext.database.GetCollection<User>("Chamomile");
            string retname = "Quest";
            var filter = new BsonDocument();
            if (s == true)
            {
                filter = new BsonDocument("Email", user.Email);
            }
            else
            {
                filter = new BsonDocument("$and", new BsonArray
                {
                    new BsonDocument("Name", user.Name),
                    new BsonDocument("Password", user.Password),
                    new BsonDocument("Email", user.Email)
                });
            }
            var list = userCollection.Find(filter).ToList();
            foreach (var doc in list)
            {
                retname = doc.Name;
            }
            return retname;
        }
    }
    
}