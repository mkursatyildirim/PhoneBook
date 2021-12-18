using PhoneBook.API.Dto;

namespace PhoneBook.API.Services
{
    public interface IPersonService
    {
        Task<ReturnDto> AddPerson(PersonDto personDto);
        Task<ReturnDto> DeletePerson(Guid personId);
        Task<IEnumerable<PersonDto>> GetAllPersons();
    }
}
