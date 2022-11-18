using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBPractice.Model
{
    public class DBConn
    {
        public string? ConnectionString { get; set; }
        public string? DatabaseName { get; set; }
        public MongoClient? MongoClient { get; set; }
        public IMongoDatabase? MongoDatabase { get; set; }
       
    }
}
