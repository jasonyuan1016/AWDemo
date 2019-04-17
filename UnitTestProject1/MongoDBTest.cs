using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;

namespace UnitTestProject1
{
    [TestClass]
    public class MongoDBTest
    {
        string connStr = "mongodb://172.16.1.52:27018";

        [TestMethod]
        public void ConnectionDB()
        {
            var client = new MongoClient(connStr);
            var database = client.GetDatabase("news");
            var collection = database.GetCollection<BsonDocument>("list");
            var filter = Builders<BsonDocument>.Filter.Eq("time", "2019-04-11");
            var result = collection.Find(filter).ToList();
            
            var s = result.Count;
            Assert.AreNotEqual(s, 0);
        }
    }
}
