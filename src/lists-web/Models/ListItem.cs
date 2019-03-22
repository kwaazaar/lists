using System;
using System.ComponentModel.DataAnnotations;

namespace list.Models
{
    public class ListItem
    {
        public Guid Id { get; set; }
        [Required]
        public Guid ListId { get; set; }
        [Required]
        public string Question { get; set; }
        [Required]
        public string Answer { get; set; }
        //public ListItemValue Value { get; set; }
   }
}