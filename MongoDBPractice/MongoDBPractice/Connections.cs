using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDBPractice.Model;

namespace MongoDBPractice
{
    public class Connections
    {
        public static readonly DBConn ConInfo = new DBConn();
        public static void LoadConnection()
        {
            try
            {
                if (string.IsNullOrEmpty(ConInfo.ConnectionString) || string.IsNullOrWhiteSpace(ConInfo.ConnectionString))
                {
                    ConInfo.ConnectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                    ConInfo.DatabaseName = MongoUrl.Create(ConInfo.ConnectionString).DatabaseName;
                    ConInfo.MongoClient = new MongoClient(ConInfo.ConnectionString);
                    ConInfo.MongoDatabase = ConInfo.MongoClient.GetDatabase(ConInfo.DatabaseName);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
