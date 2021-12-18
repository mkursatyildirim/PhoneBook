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
    }
}
