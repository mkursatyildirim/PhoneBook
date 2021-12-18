using PhoneBook.API.Dto;

namespace PhoneBook.API.Services
{
    public interface IPersonService
    {
        Task<ReturnDto> AddPerson(PersonDto personDto);
    }
}
