using list.Models;
using list.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using model = list.Models;

namespace list.Managers
{
    public class ListManager : IListManager
    {
        //private static List<ListModel> Lists = new List<ListModel>
        //{
        //    new ListModel { Id = 1, Name = "Engels", Items = new List<ListItem>
        //        {
        //            new ListItem { Id = 1, ListId = 1, Question = "Hello", Answer = "Hallo" },
        //            new ListItem { Id = 2, ListId = 1, Question = "Goodbye", Answer = "Tot ziens" },
        //            new ListItem { Id = 3, ListId = 1, Question = "Morning", Answer = "Morgen" },
        //            new ListItem { Id = 4, ListId = 1, Question = "Afternoon", Answer = "Middag" },
        //        }
        //    },
        //    new ListModel { Id = 2, Name = "Frans", Items = new List<ListItem>() },
        //};

        private readonly IListStorage _storage;

        public ListManager(IListStorage storage)
        {
            _storage = storage;
        }

        public Task<ListModel> GetList(string userId, int id)
        {
            return _storage.GetList(userId, id);
        }

        public Task<ListModel> AddList(string userId, ListModel list)
        {
            return _storage.AddList(userId, list);
        }

        public Task<bool> DeleteList(string userId, int listId)
        {
            return _storage.DeleteList(userId, listId);
        }

        public Task<model.ListItem> UpsertListItem(string userId, model.ListItem listItem)
        {
            if (listItem.ListId == default(int)) throw new ArgumentException("No listId set");
            return _storage.UpsertListItem(userId, listItem);
        }

        public Task<IEnumerable<ListSummary>> GetAllLists(string userId)
        {
            return _storage.GetAllLists(userId);
        }

        public Task<bool> DeleteListItem(string userId, int listId, int listItemId)
        {
            return _storage.DeleteListItem(userId, listId, listItemId);
        }
    }
}
