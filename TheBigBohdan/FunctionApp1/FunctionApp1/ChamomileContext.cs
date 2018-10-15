using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using System.Configuration;
namespace FunctionApp1
{
    public class ChamomileContext
    {
        public IMongoDatabase database;
        public ChamomileContext()
        {
            var mongoClient = new MongoClient("mongodb://zero_cu:PA$$WORD@localhost:27017/usersdb");
            database = mongoClient.GetDatabase("usersdb");
        }
    }
}