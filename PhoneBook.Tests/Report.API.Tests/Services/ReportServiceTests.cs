using PhoneBook.Tests.Helpers;
using Report.API.Entities.Context;
using Report.API.Services;
using System;
using System.Threading.Tasks;
using Xunit;

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
    }
}
