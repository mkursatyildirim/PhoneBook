using PhoneBook.API.Dto;

namespace PhoneBook.API.Services
{
    public interface IPersonService
    {
        Task<ReturnDto> AddPerson(PersonDto personDto);
        Task<ReturnDto> DeletePerson(Guid personId);
        Task<IEnumerable<PersonDto>> GetAllPersons();
        Task<PersonDto> GetPerson(Guid personId);
        Task<PersonDetailDto> GetPersonDetail(Guid personId);
    }
}
