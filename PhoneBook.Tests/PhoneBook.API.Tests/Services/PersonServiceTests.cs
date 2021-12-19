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

            await context.SaveChangesAsync();

            var service = new PersonService(context);

            var result = await service.AddPerson(new PersonDto()
            {
                Name = "Jane",
                Surname = "Doe",
                Company = "Bla Bla Inc."
            });

            Assert.Equal(3, context.Persons.Count());
        }

        [Fact]
        public async Task DeletePerson_With_Valid_Params_Should_Delete_Person()
        {
            var context = new PhoneBookContext(TestHelper.GetPhoneBookContextForInMemoryDb());

            var deletedPersonId = Guid.NewGuid();

            context.Persons.Add(new Person()
            {
                UUID = deletedPersonId,
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

            await context.SaveChangesAsync();

            var service = new PersonService(context);

            var result = await service.DeletePerson(deletedPersonId);
            
            Assert.Equal(1, context.Persons.Count());
        }

        [Fact]
        public async Task GetAllPersons_Should_Get_All_Persons()
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

            await context.SaveChangesAsync();

            var service = new PersonService(context);

            var result = await service.GetAllPersons();

            Assert.Equal(2, context.Persons.Count());
        }

        [Fact]
        public async Task GetPerson_With_Valid_Params_Should_Get_Person()
        {
            var context = new PhoneBookContext(TestHelper.GetPhoneBookContextForInMemoryDb());
            
            var searchPersonId = Guid.NewGuid();

            context.Persons.Add(new Person()
            {
                UUID = searchPersonId,
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

            await context.SaveChangesAsync();

            var service = new PersonService(context);

            var result = await service.GetPerson(searchPersonId);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetPersonDetail_With_Valid_Params_Should_Get_Person_And_Details()
        {
            var context = new PhoneBookContext(TestHelper.GetPhoneBookContextForInMemoryDb());

            var searchPersonId = Guid.NewGuid();

            context.Persons.Add(new Person()
            {
                UUID = searchPersonId,
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

            await context.SaveChangesAsync();

            var service = new PersonService(context);

            var result = await service.GetPersonDetail(searchPersonId);

            Assert.NotNull(result);
        }
    }
}
