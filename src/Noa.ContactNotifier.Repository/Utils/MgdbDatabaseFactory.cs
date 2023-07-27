using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noa.ContactNotifier.Repository.Utils;

public class MgdbDatabaseFactory
{
    public static IMongoDatabase Create(string connectionString, string databaseName)
    {
        var mongoClient = new MongoClient(connectionString);
        return mongoClient.GetDatabase(databaseName);
    }
}
