using PhoneBook.API.Dto;
using PhoneBook.API.Entities;
using PhoneBook.API.Entities.Context;
using PhoneBook.API.Enums;
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
    public class ContactInformationServiceTests
    {
        [Fact]
        public async Task AddContactInformation_With_Valid_Params_Should_Add_New_Contact_Information()
        {
            var context = new PhoneBookContext(TestHelper.GetPhoneBookContextForInMemoryDb());

            var personId = Guid.NewGuid();
            context.Persons.Add(new Person()
            {
                UUID = personId,
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

            var service = new ContactInformationService(context);

            var result = await service.AddContactInformation(personId, new ContactInformationDto()
            {
                InformationType = InformationType.Location,
                InformationContent = "Mersin"
            });

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task AddContactInformation_With_Invalid_Params_Should_Not_Add_New_Contact_Information()
        {
            var context = new PhoneBookContext(TestHelper.GetPhoneBookContextForInMemoryDb());

            var personId = Guid.NewGuid();
            context.Persons.Add(new Person()
            {
                UUID = personId,
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

            var service = new ContactInformationService(context);

            var result = await service.AddContactInformation(Guid.Empty, new ContactInformationDto()
            {
                InformationType = InformationType.Location,
                InformationContent = "Mersin"
            });

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task DeleteContactInformation_With_Valid_Params_Should_Delete_Contact_Information()
        {
            var context = new PhoneBookContext(TestHelper.GetPhoneBookContextForInMemoryDb());

            var contactInformationId = Guid.NewGuid();
            context.ContactInformations.Add(new ContactInformation()
            {
                UUID = contactInformationId,
                InformationType = InformationType.Location,
                InformationContent = "Mersin"
            });

            await context.SaveChangesAsync();

            var service = new ContactInformationService(context);

            var result = await service.DeleteContactInformation(contactInformationId);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task DeleteContactInformation_With_Invalid_Params_Should_Not_Delete_Contact_Information()
        {
            var context = new PhoneBookContext(TestHelper.GetPhoneBookContextForInMemoryDb());

            var contactInformationId = Guid.NewGuid();
            context.ContactInformations.Add(new ContactInformation()
            {
                UUID = contactInformationId,
                InformationType = InformationType.Location,
                InformationContent = "Mersin"
            });

            await context.SaveChangesAsync();

            var service = new ContactInformationService(context);

            var result = await service.DeleteContactInformation(Guid.Empty);

            Assert.False(result.IsSuccess);
        }
    }
}
