using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using list.Models;

namespace list.Managers
{
    public interface IListManager
    {
        Task<IEnumerable<ListSummary>> GetAllLists(string userId);
        Task<ListModel> GetList(string userId, Guid id);

        Task<ListModel> AddList(string userId, ListModel list);
        Task<ListItem> UpsertListItem(string userId, ListItem listItem);
        Task<bool> DeleteListItem(string userId, Guid listId, Guid listItemId);
        Task<bool> DeleteList(string userId, Guid listId);
    }
}