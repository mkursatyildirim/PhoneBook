using PhoneBook.API.Dto;
using PhoneBook.API.Entities;
using PhoneBook.API.Entities.Context;
using PhoneBook.API.Services;
using PhoneBook.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PhoneBook.Tests.PhoneBook.API.Tests.Services
{
    public class PersonServiceTests
    {
        [Fact]
        public async Task AddPerson_With_Valid_Params_Should_Add_New_Person()
        {
            var context = new PhoneBookContext(TestHelper.GetPhoneBookContextForInMemoryDb());

            context.Persons.Add(new Person()
            {
                Name = "Muhammet Kürşat",
                Surname = "YILDIRIM",
                Company = "Kardelen Yazılım"
            });

            context.Persons.Add(new Person()
            {
                Name = "John",
                Surname = "Doe",
                Company = "Bla Bla Inc."
            });

            var service = new PersonService(context);

            var result = await service.AddPerson(new PersonDto()
            {
                Name = "Jane",
                Surname = "Doe",
                Company = "Bla Bla Inc."
            });

            Assert.Equal(3, context.Persons.Count());
        }
    }
}
