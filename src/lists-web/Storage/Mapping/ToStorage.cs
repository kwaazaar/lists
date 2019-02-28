using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using storage = list.Storage;
using model = list.Models;

namespace list.Storage.Mapping
{
    public static class ListMappings
    {
        public static storage.List Map(this model.ListModel list, string userId)
        {
            return new storage.List()
            {
                UserId = userId,
                Id = list.Id,
                Name = list.Name,
                Items = list.Items.Select(li => new storage.ListItem()
                {
                    Id = li.Id,
                    Question = li.Question,
                    Answer = li.Answer,
                }).ToList(),
            };
        }

        public static model.ListModel Map(this storage.List l)
        {
            return new model.ListModel()
            {
                Id = l.Id,
                Name = l.Name,
                Items = l.Items.Select(li => new model.ListItem()
                {
                    Id = li.Id,
                    Question = li.Question,
                    Answer = li.Answer,
                    ListId = l.Id,
                }).ToList(),
            };
        }
    }
}
