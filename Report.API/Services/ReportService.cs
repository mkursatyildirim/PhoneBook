using Report.API.Entities.Context;
using Report.API.Enums;
using Entites = Report.API.Entities;
namespace Report.API.Services
{
    public class ReportService : IReportService
    {
        private readonly ReportContext _context;

        public ReportService(ReportContext context)
        {
            _context = context;
        }

        public async Task GenerateStatisticsReport(Guid reportId)
        {
            await _context.SaveChangesAsync();
            return;
        }

        public async Task<Guid> CreateNewReport()
        {
            var report = new Entities.Report
            {
                RequestDate = DateTime.UtcNow,
                ReportStatus = ReportStatus.Preparing
            };

            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();

            return report.UUID;
        }
    }
}
