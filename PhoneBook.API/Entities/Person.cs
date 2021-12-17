using PhoneBook.API.Entities.Base;

namespace PhoneBook.API.Entities
{
    public class Person : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public virtual List<ContactInformation> ContactInformations { get; set; }
    }
}
