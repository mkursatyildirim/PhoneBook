using Moq;
using PhoneBook.Tests.Helpers;
using Report.API.Controllers;
using Report.API.Services;
using System.Threading.Tasks;
using Xunit;

namespace PhoneBook.Tests.Report.API.Tests.Controllers
{
    public class ReportsControllerTests
    {
        [Fact]
        public async Task ReportRequest_Should_Return_200()
        {
            var mockReportService = new Mock<IReportService>();

            var personsController = new ReportsController(mockReportService.Object);

            var result = await personsController.ReportRequest();

            Assert.Equal(200, TestHelper.GetStatusCodeFromActionResult(result));
        }
    }
}
