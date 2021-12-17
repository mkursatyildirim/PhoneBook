using System.ComponentModel.DataAnnotations;

namespace PhoneBook.API.Entities.Base
{
    public class BaseEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    }
}
