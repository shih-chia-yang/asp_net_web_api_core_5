using System.ComponentModel.DataAnnotations;

namespace todo.domain.Models
{
    public class TodoItem
    {
        /// <summary>
        /// 代碼
        /// </summary>
        /// <value></value>
        [Key]
        public int Id{ get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        /// <value></value>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 事件
        /// </summary>
        /// <value></value>
        [StringLength(150)]
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