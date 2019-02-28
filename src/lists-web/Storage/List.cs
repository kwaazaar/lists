using System.Collections.Generic;

namespace list.Storage
{
    public class List
    {
        public string UserId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ListItem> Items { get; set; } = new List<ListItem>();
    }

    public class ListItem
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
