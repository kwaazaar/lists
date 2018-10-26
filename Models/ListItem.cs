namespace list.Models
{
    public class ListItem
    {
        public int Id { get; set; }
        public int ListId { get; set; }
        public string Question { get; set; }
        public ListItemValue Value { get; set; }
   }
}