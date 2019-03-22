using System;
using System.ComponentModel.DataAnnotations;

namespace list.Models
{
    public class ListSummary
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int ItemCount { get; set; }
    }
}
