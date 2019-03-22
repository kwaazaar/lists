using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace list.Models
{
    public class ListModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<ListItem> Items { get; set; } = new List<ListItem>();
    }
}
