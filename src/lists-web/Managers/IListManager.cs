using System.Collections.Generic;
using list.Models;

namespace list.Managers
{
    public interface IListManager
    {
        IEnumerable<ListSummary> GetAllLists();
        ListModel GetList(int id);

        ListModel AddList(ListModel list);
        ListItem UpsertListItem(ListItem listItem);
        bool DeleteListItem(int listId, int listItemId);
        bool DeleteList(int listId);
    }
}