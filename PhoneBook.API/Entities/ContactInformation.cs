using PhoneBook.API.Entities.Base;
using PhoneBook.API.Enums;

namespace PhoneBook.API.Entities
{
    public class ContactInformation : BaseEntity
    {
        public InformationType InformationType { get; set; }
        public string InformationContent { get; set; }
        public virtual Person Person { get; set; }
        public Guid PersonUUID { get; set; }
    }
}
