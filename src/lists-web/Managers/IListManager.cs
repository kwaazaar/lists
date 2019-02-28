using System.Collections.Generic;
using System.Threading.Tasks;
using list.Models;

namespace list.Managers
{
    public interface IListManager
    {
        Task<IEnumerable<ListSummary>> GetAllLists(string userId);
        Task<ListModel> GetList(string userId, int id);

        Task<ListModel> AddList(string userId, ListModel list);
        Task<ListItem> UpsertListItem(string userId, ListItem listItem);
        Task<bool> DeleteListItem(string userId, int listId, int listItemId);
        Task<bool> DeleteList(string userId, int listId);
    }
}