using list.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace list.Managers
{
    public class ListManager : IListManager
    {
        private static List<ListModel> Lists = new List<ListModel>
        {
            new ListModel { Id = 1, Name = "Engels", Items = new List<ListItem>
                {
                    new ListItem { Id = 1, ListId = 1, Question = "Hello", Answer = "Hallo" },
                    new ListItem { Id = 2, ListId = 1, Question = "Goodbye", Answer = "Tot ziens" },
                    new ListItem { Id = 3, ListId = 1, Question = "Morning", Answer = "Morgen" },
                    new ListItem { Id = 4, ListId = 1, Question = "Afternoon", Answer = "Middag" },
                }
            },
            new ListModel { Id = 2, Name = "Frans", Items = new List<ListItem>() },
        };

        public ListModel GetList(int id)
        {
            return Lists.FirstOrDefault(l => l.Id == id);
        }

        public ListModel AddList(ListModel list)
        {
            var maxListId = Lists.Max(l => l.Id);
            list.Id = maxListId + 1;
            Lists.Add(list);
            return list;
        }

        public ListItem UpsertListItem(ListItem listItem)
        {
            if (listItem.ListId == default(int)) throw new ArgumentException("No listId set");

            var list = GetList(listItem.ListId);
            if (list == null) throw new ArgumentOutOfRangeException("List not found");

            if (listItem.Id == default(int)) // Add
            {
                var maxId = list.Items.Count > 0 ? list.Items.Max(i => i.Id) : 0;

                listItem.Id = maxId + 1;
                list.Items.Add(listItem);
            }
            else // Update
            {
                var existingItem = list.Items.FirstOrDefault(i => i.Id == listItem.Id);
                if (existingItem == null) throw new ArgumentOutOfRangeException("ListItem not found");
                list.Items.Remove(existingItem);
                list.Items.Add(listItem);
                list.Items = list.Items.OrderBy(i => i.Id).ToList();
            }

            return listItem;
        }

        public IEnumerable<ListSummary> GetAllLists()
        {
            return Lists.Select(l => new ListSummary { Id = l.Id, Name = l.Name, ItemCount = l.Items.Count }).ToList();
        }
    }
}
