using PhoneBook.API.Enums;

namespace PhoneBook.API.Entities
{
    public class ContactInformation
    {
        public InformationType InformationType { get; set; }
        public string InformationContent { get; set; }
        public virtual Person Person { get; set; }
        public Guid PersonUUID { get; set; }
    }
}
