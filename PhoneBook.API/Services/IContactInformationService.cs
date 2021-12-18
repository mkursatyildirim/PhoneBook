using PhoneBook.API.Dto;

namespace PhoneBook.API.Services
{
    public interface IContactInformationService
    {
        Task<ReturnDto> AddContactInformation(Guid personId, ContactInformationDto contactInformationDto);
        Task<ReturnDto> DeleteContactInformation(Guid contactInformationId);
    }
}
