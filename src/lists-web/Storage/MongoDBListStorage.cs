using list.Storage.Mapping;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using model = list.Models;
using storage = list.Storage;

namespace list.Storage
{
    public class MongoDBListStorage : IListStorage
    {
        const string COLLECTION_LIST = "list";
        const string DBNAME = "listdb";

        public MongoDBListStorage()
        {
        }


        public async Task<model.ListModel> AddList(string userId, model.ListModel list)
        {
            var insertOptions = new InsertOneOptions
            {
                BypassDocumentValidation = false,
            };

            var storageList = list.Map(userId);
            await MongoDbUtil.GetCollection<List>(DBNAME, COLLECTION_LIST).InsertOneAsync(storageList, insertOptions);
            return list;
        }

        public async Task<bool> DeleteList(string userId, Guid listId)
        {
            var deleteResult = await MongoDbUtil.GetCollection<storage.List>(DBNAME, COLLECTION_LIST)
                .DeleteOneAsync<storage.List>(l => l.UserId == userId && l.Id == listId);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<bool> DeleteListItem(string userId, Guid listId, Guid listItemId)
        {
            var list = await GetList(userId, listId);
            var removed = (list.Items.RemoveAll(li => li.Id == listItemId) > 0);
            if (removed)
            {
                var storageList = list.Map(userId);
                await UpdateStorageList(storageList);
            }

            return removed;
        }

        public Task<IEnumerable<model.ListSummary>> GetAllLists(string userId)
        {
            var q = MongoDbUtil.GetCollection<storage.List>(DBNAME, COLLECTION_LIST).AsQueryable()
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

        public async Task<model.ListModel> GetList(string userId, Guid id)
        {
            var storageList = await Task.FromResult(MongoDbUtil.GetCollection<storage.List>(DBNAME, COLLECTION_LIST).AsQueryable()
                .Where(l => l.UserId == userId)
                .Where(l => l.Id == id)
                .FirstOrDefault()); // SingleOrDefault

            return storageList?.Map();
        }

        public async Task<model.ListItem> UpsertListItem(string userId, model.ListItem listItem)
        {
            var list = await GetList(userId, listItem.ListId);
            if (listItem.Id == Guid.Empty)
                listItem.Id = Guid.NewGuid();

            var existingItem = list.Items.FirstOrDefault(li => li.Id == listItem.Id);
            if (existingItem != null)
            {
                existingItem.Question = listItem.Question;
                existingItem.Answer = listItem.Answer;
            }
            else
            {
                list.Items.Add(listItem);
            }
            var storageList = list.Map(userId);
            var listUpdated = await UpdateStorageList(storageList);

            return existingItem ?? listItem;
        }

        public async Task<storage.List> UpdateStorageList(storage.List list)
        {
            var col = MongoDbUtil.GetCollection<storage.List>(DBNAME, COLLECTION_LIST);

            var builder = Builders<storage.List>.Filter;
            var filters = builder.Eq(sl => sl.Id, list.Id) & builder.Eq(sl => sl.UserId, list.UserId);
            var result = await col.ReplaceOneAsync(filters, list);
            if (result.ModifiedCount != 1) throw new InvalidOperationException("ReplaceOne failed");

            return col.AsQueryable().First(l => l.Id == list.Id);
        }
    }
}
