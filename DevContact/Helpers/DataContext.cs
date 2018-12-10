using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevContact.Models;
using MongoDB.Driver;

namespace DevContact.Helpers
{
    public class DataContext
    {
        public MongoDatabase Database;
        public DataContext()
        {
            string connectionString = @"";
            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            MongoClient client = new MongoClient(settings);
            MongoServer server = client.GetServer();
            Database = server.GetDatabase("");
        }

        public MongoCollection<Developer> Developer => Database.GetCollection<Developer>(typeof(Developer).Name.ToLower());
    }
}
