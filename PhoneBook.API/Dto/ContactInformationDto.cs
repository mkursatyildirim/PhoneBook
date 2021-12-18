using PhoneBook.API.Enums;

namespace PhoneBook.API.Dto
{
    public class ContactInformationDto
    {
        public Guid UUID { get; set; }
        public InformationType InformationType { get; set; }
        public string InformationContent { get; set; }
    }
}
