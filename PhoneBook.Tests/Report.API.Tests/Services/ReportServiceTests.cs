using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using PhoneBook.Tests.Helpers;
using Report.API.Constants;
using Report.API.Entities.Context;
using Report.API.Enums;
using Report.API.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Entities = Report.API.Entities;

namespace PhoneBook.Tests.Report.API.Tests.Services
{
    public class ReportServiceTests
    {
        [Fact]
        public async Task CreateNewReport_Should_Create_Report()
        {
            var context = new ReportContext(TestHelper.GetReportContextForInMemoryDb());

            var service = new ReportService(context, null, null, null);

            var result = await service.CreateNewReport();

            Assert.True(result != Guid.Empty);
        }

        [Fact]
        public async Task GetAllReports_Should_Get_Reports()
        {
            var context = new ReportContext(TestHelper.GetReportContextForInMemoryDb());

            context.Reports.Add(new Entities.Report()
            {
                ReportStatus = ReportStatus.Preparing
            });

            context.Reports.Add(new Entities.Report()
            {
                ReportStatus = ReportStatus.Completed
            });

            await context.SaveChangesAsync();

            var service = new ReportService(context, null, null, null);

            var result = await service.GetAllReports();

            Assert.Equal(2, context.Reports.Local.Count);
        }

        [Fact]
        public async Task GetReportDetail_With_Valid_Params_Should_Get_Report_Detail()
        {
            var context = new ReportContext(TestHelper.GetReportContextForInMemoryDb());

            var reportId = Guid.NewGuid();
            context.Reports.Add(new Entities.Report()
            {
                UUID = reportId,
                ReportStatus = ReportStatus.Preparing
            });

            context.ReportDetails.Add(new Entities.ReportDetail()
            {
                Location = "Mersin",
                PersonCount = 1,
                PhoneNumberCount = 1,
                ReportId = reportId
            });

            await context.SaveChangesAsync();

            var service = new ReportService(context, null, null, null);

            var result = await service.GetReportDetail(reportId);

            Assert.NotNull(result.Report);
            Assert.NotNull(result.ReportDetails);
        }

        [Fact]
        public async Task GenerateStatisticsReport_If_Report_Not_Found_Should_Throw_Error()
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("[{'uuid': 'a5ba5d9c-f6fa-4466-9ef6-508592438fc9','informationType':2,'informationContent':'Mersin', 'personId': 'b890413c-2dca-499a-8578-5cfb14e0b1eb'}]"),
                });

            var mockFactory = new Mock<IHttpClientFactory>();

            var client = new HttpClient(mockHttpMessageHandler.Object);
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            IOptions<ReportSettings> settings = Options.Create<ReportSettings>(new ReportSettings()
            {
                PhoneBookApiUrl = "test_url"
            });

            
            var serviceCollection = new ServiceCollection().AddLogging();
            var logger = serviceCollection.BuildServiceProvider().GetService<ILogger<ReportService>>();

            var context = new ReportContext(TestHelper.GetReportContextForInMemoryDb());

            var service = new ReportService(context, mockFactory.Object, logger, settings);

            Func<Task> func = () =>  service.GenerateStatisticsReport(Guid.Parse("a5ba5d9c-f6fa-4466-9ef6-508592438fc9"));             //assert
            Exception exception = await Assert.ThrowsAsync<Exception>(func);
        }
    }
}
