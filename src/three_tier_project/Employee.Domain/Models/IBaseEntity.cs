using System.ComponentModel.DataAnnotations;

namespace Employee.Domain.Models
{
    public interface IBaseEntity
    {
        [Key]
        int Id { get; set; }
    }
}