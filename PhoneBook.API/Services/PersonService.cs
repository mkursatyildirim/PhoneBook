using Microsoft.EntityFrameworkCore;
using PhoneBook.API.Dto;
using PhoneBook.API.Entities;
using PhoneBook.API.Entities.Context;
using PhoneBook.API.Services.Base;

namespace PhoneBook.API.Services
{
    public class PersonService : BaseService, IPersonService
    {
        public PersonService(PhoneBookContext context) : base(context)
        {
        }

        public async Task<ReturnDto> AddPerson(PersonDto personDto)
        {
            var person = new Person()
            {
                Name = personDto.Name,
                Surname = personDto.Surname,
                Company = personDto.Company
            };

            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();

            if (person.UUID != Guid.Empty)
            {
                return new ReturnDto
                {
                    IsSuccess = true,
                    Message = "Kişi eklendi.",
                    Data = person
                };
            }

            return new ReturnDto
            {
                IsSuccess = false,
                Message = "Kişi eklenemedi.",
                Data = null
            };
        }

        public async Task<ReturnDto> DeletePerson(Guid personId)
        {
            var result = await _context.Persons.Where(p => p.UUID == personId).FirstOrDefaultAsync();

            if (result == null)
            {
                return new ReturnDto
                {
                    IsSuccess = false,
                    Message = "Silinecek kişi bulunamadı.",
                    Data = null
                };
            }

            _context.Persons.Remove(result);
            await _context.SaveChangesAsync();

            return new ReturnDto
            {
                IsSuccess = true,
                Message = "Kişi silindi.",
                Data = result
            };
        }

        public async Task<IEnumerable<PersonDto>> GetAllPersons()
        {
            return await _context.Persons.Select(p => new PersonDto
            {
                UUID = p.UUID,
                Name = p.Name,
                Surname = p.Surname,
                Company = p.Company,
            }).ToListAsync();
        }

        public async Task<PersonDto> GetPerson(Guid personId)
        {
            return await _context.Persons.Where(p => p.UUID == personId).Select(p => new PersonDto
            {
                UUID = p.UUID,
                Name = p.Name,
                Surname = p.Surname,
                Company = p.Company
            }).FirstOrDefaultAsync();
        }

        public async Task<PersonDetailDto> GetPersonDetail(Guid personId)
        {
            var result = await _context.Persons.Where(p => p.UUID == personId).Select(p => new PersonDetailDto
            {
                Person = new PersonDto()
                {
                    UUID = p.UUID,
                    Name = p.Name,
                    Surname = p.Surname,
                    Company = p.Company
                },
                ContactInformations = p.ContactInformations.Select(ci => new ContactInformationDto
                {
                    UUID = ci.UUID,
                    InformationType = ci.InformationType,
                    InformationContent = ci.InformationContent
                }).ToList()
            }).FirstOrDefaultAsync();

            return result;
        }
    }
}
