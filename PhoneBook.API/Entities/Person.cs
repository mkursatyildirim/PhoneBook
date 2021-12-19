using PhoneBook.API.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.API.Entities
{
    [Table("persons")]
    public class Person : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public virtual List<ContactInformation> ContactInformations { get; set; }
    }
}
