namespace todo.domain.Models
{
    public class TodoItem
    {
        public int Id{ get; set; }

        public string Name { get; set; }

        public string Event { get; set; }

        public TodoItem()
        {
            
        }

        public static TodoItem Create(int _id,string _name,string _event)
        {
            return new TodoItem() { Id = _id, Name = _name, Event = _event };
        }
    }
}