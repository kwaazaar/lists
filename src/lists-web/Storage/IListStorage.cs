using model=list.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace list.Storage
{
    public interface IListStorage
    {
        Task<IEnumerable<model.ListSummary>> GetAllLists(string userId);

        Task<model.ListModel> GetList(string userId, Guid id);
        Task<model.ListModel> AddList(string userId, model.ListModel list);
        Task<bool> DeleteList(string userId, Guid listId);

        Task<model.ListItem> UpsertListItem(string userId, model.ListItem listItem);
        Task<bool> DeleteListItem(string userId, Guid listId, Guid listItemId);
    }
}

