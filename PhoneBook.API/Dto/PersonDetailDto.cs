namespace PhoneBook.API.Dto
{
    public class PersonDetailDto
    {
        public PersonDto Person { get; set; }
        public List<ContactInformationDto> ContactInformations { get; set; } = new();
    }
}
