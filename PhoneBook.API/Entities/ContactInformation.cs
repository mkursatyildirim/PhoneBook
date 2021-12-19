using PhoneBook.API.Entities.Base;
using PhoneBook.API.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.API.Entities
{
    [Table("contact_informations")]
    public class ContactInformation : BaseEntity
    {
        public InformationType InformationType { get; set; }
        public string InformationContent { get; set; }
        public Guid PersonUUID { get; set; }
    }
}
