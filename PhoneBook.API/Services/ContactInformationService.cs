using Microsoft.EntityFrameworkCore;
using PhoneBook.API.Dto;
using PhoneBook.API.Entities;
using PhoneBook.API.Entities.Context;
using PhoneBook.API.Services.Base;

namespace PhoneBook.API.Services
{
    public class ContactInformationService : BaseService, IContactInformationService
    {
        public ContactInformationService(PhoneBookContext context) : base(context)
        {
        }

        public async Task<ReturnDto> AddContactInformation(Guid personId, ContactInformationDto contactInformationDto)
        {
            var person = await _context.Persons.Where(p => p.UUID == personId).FirstOrDefaultAsync();

            if (person == null)
            {
                return new ReturnDto()
                {
                    IsSuccess = false,
                    Message = "İletişim bilgisi eklenecek kişi bulunamadı.",
                    Data = null
                };
            }

            var contactInformation = new ContactInformation()
            {
                PersonUUID = personId,
                InformationType = contactInformationDto.InformationType,
                InformationContent = contactInformationDto.InformationContent,
            };

            await _context.AddAsync(contactInformation);
            await _context.SaveChangesAsync();

            return new ReturnDto()
            {
                IsSuccess = true,
                Message = $"{person.Name}  {person.Surname} adlı kişi için iletişim bilgileri eklendi.",
                Data = contactInformation
            };
        }

        public async Task<ReturnDto> DeleteContactInformation(Guid contactInformationId)
        {
            var contactInformation = await _context.ContactInformations.Where(ci => ci.UUID == contactInformationId).FirstOrDefaultAsync();

            if (contactInformation == null)
            {
                return new ReturnDto()
                {
                    IsSuccess = false,
                    Message = "Silinecek iletişim bilgisi bulunamadı.",
                    Data = null
                };
            }

            _context.ContactInformations.Remove(contactInformation);
            await _context.SaveChangesAsync();

            return new ReturnDto()
            {
                IsSuccess = true,
                Message = "İletişim bilgisi silindi.",
                Data = contactInformation
            };
        }
    }
}
