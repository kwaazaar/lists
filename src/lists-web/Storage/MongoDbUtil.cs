using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace list.Storage
{
    public static class MongoDbUtil
    {
        public static Task<List<T>> ToMongoListAsync<T>(this IQueryable<T> mongoQueryOnly)
        {
            return ((IMongoQueryable<T>)mongoQueryOnly).ToListAsync();
        }
        //public static IAsyncEnumerable<T> ToAsyncEnumerableA<T>(this IQueryable<T> mongoQueryOnly)
        //{
        //    return ((IMongoQueryable<T>)mongoQueryOnly).ToAsyncEnumerable();
        //}


        public static MongoClient GetMongoClient()
        {
            /* Other way:
            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(host, 10255);
            settings.UseSsl = true;
            settings.SslSettings = new SslSettings();
            settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;

            MongoIdentity identity = new MongoInternalIdentity(dbName, userName);
            MongoIdentityEvidence evidence = new PasswordEvidence(password);

            settings.Credential = new MongoCredential("SCRAM-SHA-1", identity, evidence);

            MongoClient client = new MongoClient(settings);
             */

            // TODO: Pass connectionstring and sslEnabled:bool
            string connectionString = @"mongodb://docati:A8tU1zSGzVf5AEEPmVuLMiFJ8SKqSG4hOo4S9eM7ctDEiQKfJkXWHyHROJ6BdtSM3brUTKTOktEfUxAF8kL2xQ==@docati.documents.azure.com:10255/?ssl=true&replicaSet=globaldb";
            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            return new MongoClient(settings);
        }

        public static IMongoDatabase GetDatabase(string dbName)
        {
            return GetMongoClient().GetDatabase(dbName);
        }

        // Collection must have been created manually in the portal, because of the required sharding key (not possible with MongoDB api)
        public static IMongoCollection<TCol> GetCollection<TCol>(string dbName, string collectionName)
        {
            var db = GetDatabase(dbName);
            var col = db.GetCollection<TCol>(collectionName);
            return col;
        }
    }
}
