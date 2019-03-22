using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;

namespace list.Storage
{
    public class List
    {
        [BsonRequired] // shard partitionkey
        public string UserId { get; set; }

        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public List<ListItem> Items { get; set; } = new List<ListItem>();
    }

    public class ListItem
    {
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }

        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
