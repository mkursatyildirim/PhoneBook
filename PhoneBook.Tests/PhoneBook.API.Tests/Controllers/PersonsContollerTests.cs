using Microsoft.AspNetCore.Mvc;
using Moq;
using PhoneBook.API.Controllers;
using PhoneBook.API.Dto;
using PhoneBook.API.Enums;
using PhoneBook.API.Services;
using PhoneBook.Tests.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PhoneBook.Tests.PhoneBook.API.Tests.Controllers
{
    public class PersonsContollerTests
    {
        [Fact]
        public async Task AddPerson_With_Valid_Params_Should_Return_201()
        {
            var mockPersonService = new Mock<IPersonService>();
            mockPersonService
                .Setup(x => x.AddPerson(It.IsAny<PersonDto>()))
                .ReturnsAsync(() => new ReturnDto()
                {
                    IsSuccess = true
                });

            var personsController = new PersonsController(mockPersonService.Object, new Mock<IContactInformationService>().Object);

            var result = await personsController.AddPerson(new PersonDto()
            {
                Name = "John",
                Surname = "Doe",
                Company = "Test Inc."
            });

            Assert.Equal(201, TestHelper.GetStatusCodeFromActionResult(result));
        }

        [Fact]
        public async Task AddPerson_With_Invalid_Params_Should_Return_Null()
        {
            var mockPersonService = new Mock<IPersonService>();
            mockPersonService
                .Setup(x => x.AddPerson(It.IsAny<PersonDto>()))
                .ReturnsAsync(() => new ReturnDto()
                {
                    IsSuccess = false
                });

            var personsController = new PersonsController(mockPersonService.Object, new Mock<IContactInformationService>().Object);

            var result = await personsController.AddPerson(null);

            Assert.Null(result.Value);
        }

        [Fact]
        public async Task DeletePerson_With_Valid_Params_Should_Return_200()
        {
            var id = Guid.NewGuid();
            var mockPersonService = new Mock<IPersonService>();
            mockPersonService
                .Setup(x => x.DeletePerson(id))
                .ReturnsAsync(() => new ReturnDto()
                {
                    IsSuccess = true
                });

            var personsController = new PersonsController(mockPersonService.Object, new Mock<IContactInformationService>().Object);

            var result = await personsController.DeletePerson(id);

            Assert.Equal(200, TestHelper.GetStatusCodeFromActionResult(result));
        }

        [Fact]
        public async Task DeletePerson_With_Valid_Params_But_No_Person_Should_Return_404()
        {
            var mockPersonService = new Mock<IPersonService>();
            mockPersonService
                .Setup(x => x.DeletePerson(It.IsAny<Guid>()))
                .ReturnsAsync(() => new ReturnDto()
                {
                    IsSuccess = false
                });

            var personsController = new PersonsController(mockPersonService.Object, new Mock<IContactInformationService>().Object);

            var result = await personsController.DeletePerson(Guid.NewGuid());

            Assert.Equal(404, TestHelper.GetStatusCodeFromActionResult(result));
        }

        [Fact]
        public async Task AddContactInformation_With_Valid_Params_Should_Return_201()
        {
            var mockContactInformationService = new Mock<IContactInformationService>();

            var id = Guid.NewGuid();

            mockContactInformationService
                .Setup(x => x.AddContactInformation(id, It.IsAny<ContactInformationDto>()))
                .ReturnsAsync(() => new ReturnDto()
                {
                    IsSuccess = true
                });

            var personsController = new PersonsController(new Mock<IPersonService>().Object, mockContactInformationService.Object);


            var result = await personsController.AddContactInformation(id, new ContactInformationDto()
            {
                InformationType = InformationType.PhoneNumber,
                InformationContent = "05320000000",
                PersonId = Guid.NewGuid(),
            });

            Assert.Equal(201, TestHelper.GetStatusCodeFromActionResult(result));
        }

        [Fact]
        public async Task AddContactInformation_With_Invalid_Params_Should_Return_Null()
        {
            var mockContactInformationService = new Mock<IContactInformationService>();

            var id = Guid.NewGuid();

            mockContactInformationService
                .Setup(x => x.AddContactInformation(id, It.IsAny<ContactInformationDto>()))
                .ReturnsAsync(() => new ReturnDto()
                {
                    IsSuccess = true
                });

            var personsController = new PersonsController(new Mock<IPersonService>().Object, mockContactInformationService.Object);


            var result = await personsController.AddContactInformation(id, null);

            Assert.Null(result.Value);
        }

        [Fact]
        public async Task AddContactInformation_With_Valid_Params_But_No_Person_Should_Return_404()
        {
            var mockContactInformationService = new Mock<IContactInformationService>();

            mockContactInformationService
                .Setup(x => x.AddContactInformation(Guid.Empty, It.IsAny<ContactInformationDto>()))
                .ReturnsAsync(() => new ReturnDto()
                {
                    IsSuccess = false
                });

            var personsController = new PersonsController(new Mock<IPersonService>().Object, mockContactInformationService.Object);


            var result = await personsController.AddContactInformation(Guid.Empty, new ContactInformationDto()
            {
                InformationType = InformationType.PhoneNumber,
                InformationContent = "05320000000",
                PersonId = Guid.NewGuid(),
            });

            Assert.Equal(404, TestHelper.GetStatusCodeFromActionResult(result));
        }

        [Fact]
        public async Task DeleteContactInformation_With_Valid_Params_Should_Return_200()
        {
            var mockContactInformationService = new Mock<IContactInformationService>();

            var id = Guid.NewGuid();
            mockContactInformationService
                .Setup(x => x.DeleteContactInformation(id))
                .ReturnsAsync(() => new ReturnDto()
                {
                    IsSuccess = true
                });

            var personsController = new PersonsController(new Mock<IPersonService>().Object, mockContactInformationService.Object);


            var result = await personsController.DeleteContactInformation(id);

            Assert.Equal(200, TestHelper.GetStatusCodeFromActionResult(result));
        }

        [Fact]
        public async Task DeleteContactInformation_With_Valid_Params_But_No_Person_Should_Return_404()
        {
            var mockContactInformationService = new Mock<IContactInformationService>();

            mockContactInformationService
                .Setup(x => x.DeleteContactInformation(Guid.Empty))
                .ReturnsAsync(() => new ReturnDto()
                {
                    IsSuccess = false
                });

            var personsController = new PersonsController(new Mock<IPersonService>().Object, mockContactInformationService.Object);


            var result = await personsController.DeleteContactInformation(Guid.Empty);

            Assert.Equal(404, TestHelper.GetStatusCodeFromActionResult(result));
        }

        [Fact]
        public async Task GetAllPersons_Should_Return_200()
        {
            var mockPersonService = new Mock<IPersonService>();
            mockPersonService
                .Setup(x => x.GetAllPersons())
                .ReturnsAsync(new List<PersonDto>(){ });

            var personsController = new PersonsController(mockPersonService.Object, new Mock<IContactInformationService>().Object);

            var result = await personsController.GetAllPersons();

            Assert.Equal(200, TestHelper.GetStatusCodeFromActionResult(result));
        }

        [Fact]
        public async Task GetPerson_With_Valid_Params_Should_Return_200()
        {
            var mockPersonService = new Mock<IPersonService>();
            mockPersonService
                .Setup(x => x.GetPerson(It.IsAny<Guid>()))
                .ReturnsAsync(new PersonDto() { });

            var personsController = new PersonsController(mockPersonService.Object, new Mock<IContactInformationService>().Object);

            var result = await personsController.GetPerson(Guid.NewGuid());

            Assert.Equal(200, TestHelper.GetStatusCodeFromActionResult(result));
        }

        [Fact]
        public async Task GetPersonDetail_With_Valid_Params_Should_Return_200()
        {
            var mockPersonService = new Mock<IPersonService>();
            mockPersonService
                .Setup(x => x.GetPersonDetail(It.IsAny<Guid>()))
                .ReturnsAsync(new PersonDetailDto() { });

            var personsController = new PersonsController(mockPersonService.Object, new Mock<IContactInformationService>().Object);

            var result = await personsController.GetPersonDetail(Guid.NewGuid());

            Assert.Equal(200, TestHelper.GetStatusCodeFromActionResult(result));
        }
    }
}
