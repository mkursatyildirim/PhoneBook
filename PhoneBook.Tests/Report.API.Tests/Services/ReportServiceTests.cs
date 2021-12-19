using PhoneBook.Tests.Helpers;
using Report.API.Entities.Context;
using Report.API.Enums;
using Report.API.Services;
using System;
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
    }
}
