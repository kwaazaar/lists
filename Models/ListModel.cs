using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace list.Models
{
    public class ListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ListItem> Items { get; set; } = new List<ListItem>();
    }
}
