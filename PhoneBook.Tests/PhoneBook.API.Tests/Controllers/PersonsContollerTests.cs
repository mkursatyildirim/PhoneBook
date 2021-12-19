using Microsoft.AspNetCore.Mvc;
using Moq;
using PhoneBook.API.Controllers;
using PhoneBook.API.Dto;
using PhoneBook.API.Services;
using PhoneBook.Tests.Helpers;
using System;
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
                .ReturnsAsync(() => new ReturnDto());

            var personsController = new PersonsController(mockPersonService.Object, new Mock<IContactInformationService>().Object);

            var result = await personsController.AddPerson(null);

            Assert.Null(result.Value);
        }

        [Fact]
        public async Task DeletePerson_With_Valid_Params_Should_Return_404()
        {
            var mockPersonService = new Mock<IPersonService>();
            mockPersonService
                .Setup(x => x.DeletePerson(It.IsAny<Guid>()))
                .ReturnsAsync(() => new ReturnDto());

            var personsController = new PersonsController(mockPersonService.Object, new Mock<IContactInformationService>().Object);

            var result = await personsController.DeletePerson(Guid.NewGuid());

            Assert.Equal(404, TestHelper.GetStatusCodeFromActionResult(result));
        }
    }
}
