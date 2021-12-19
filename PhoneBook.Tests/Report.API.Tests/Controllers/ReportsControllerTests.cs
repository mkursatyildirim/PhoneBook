using Microsoft.AspNetCore.Mvc;
using Moq;
using PhoneBook.Tests.Helpers;
using Report.API.Controllers;
using Report.API.Dto;
using Report.API.Services;
using System;
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

            var reportsController = new ReportsController(mockReportService.Object);

            var result = await reportsController.ReportRequest();

            Assert.Equal(200, TestHelper.GetStatusCodeFromActionResult(result));
        }

        [Fact]
        public async Task GetAllReports_Should_Return_200()
        {
            var mockReportService = new Mock<IReportService>();

            var reportsController = new ReportsController(mockReportService.Object);

            var result = await reportsController.GetAllReports();

            Assert.Equal(200, TestHelper.GetStatusCodeFromActionResult(result));
        }

        [Fact]
        public async Task GetReportDetail_With_Valid_Params_Should_Return_200()
        {

            var reportId = Guid.NewGuid();
            var mockReportService = new Mock<IReportService>();
            mockReportService
                .Setup(x => x.GetReportDetail(reportId))
                .ReturnsAsync(() => new ReportDetailDto()
                {
                    Report = new ReportDto()
                    {
                        UUID = reportId
                    }
                });
                

            var reportsController = new ReportsController(mockReportService.Object);

            var result = await reportsController.GetReportDetail(reportId);

            Assert.Equal(200, TestHelper.GetStatusCodeFromActionResult(result));
        }

        [Fact]
        public async Task GetReportDetail_With_Invalid_Params_Should_Return_404()
        {

            var mockReportService = new Mock<IReportService>();
            mockReportService
                .Setup(x => x.GetReportDetail(It.IsAny<Guid>()))
                .ReturnsAsync(() => null);


            var reportsController = new ReportsController(mockReportService.Object);

            var result = await reportsController.GetReportDetail(Guid.Empty);

            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);

            Assert.Equal(404, notFoundResult.StatusCode);
        }
    }
}
