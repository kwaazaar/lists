using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using model = list.Models;
using storage = list.Storage;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using list.Storage.Mapping;

namespace list.Storage
{
    public class MongoDBListStorage : IListStorage
    {
        const string COLLECTION_LIST = "list";
        const string DBNAME = "listdb";

        public MongoDBListStorage()
        {
        }

        private MongoClient GetMongoClient()
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

        private IMongoCollection<TCol> GetCollection<TCol>(string dbName, string collectionName)
        {
            return GetMongoClient()
                .GetDatabase(dbName)
                .GetCollection<TCol>(collectionName);
        }

        public async Task<model.ListModel> AddList(string userId, model.ListModel list)
        {
            var insertOptions = new InsertOneOptions
            {
                BypassDocumentValidation = false,
            };

            var storageList = list.Map(userId);
            await GetCollection<List>(DBNAME, COLLECTION_LIST).InsertOneAsync(storageList, insertOptions);
            return list;
        }

        public async Task<bool> DeleteList(string userId, int listId)
        {
            var deleteResult = await GetCollection<storage.List>(DBNAME, COLLECTION_LIST)
                .DeleteOneAsync<storage.List>(l => l.UserId == userId && l.Id == listId);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public Task<bool> DeleteListItem(string userId, int listId, int listItemId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<model.ListSummary>> GetAllLists(string userId)
        {
            var q = GetCollection<storage.List>(DBNAME, COLLECTION_LIST).AsQueryable()
                .Where(l => l.UserId == userId)
                .Select(l => new model.ListSummary()
                {
                    Id = l.Id,
                    Name = l.Name,
                    ItemCount = l.Items.Count
                });

            return Task.FromResult(q
                .AsEnumerable());
        }

        public async Task<model.ListModel> GetList(string userId, int id)
        {
            var storageList = await Task.FromResult(GetCollection<storage.List>(DBNAME, COLLECTION_LIST).AsQueryable()
                .Where(l => l.UserId == userId)
                .Where(l => l.Id == id)
                .FirstOrDefault()); // SingleOrDefault

            return storageList?.Map();
        }

        public async Task<model.ListItem> UpsertListItem(string userId, model.ListItem listItem)
        {
            var list = await GetList(userId, listItem.ListId);
            if (list == null) throw new ArgumentOutOfRangeException("userId", "List not found");

            throw new NotImplementedException();
        }
    }
}
