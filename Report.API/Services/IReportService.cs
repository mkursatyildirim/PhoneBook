using Report.API.Dto;

namespace Report.API.Services
{
    public interface IReportService
    {
        Task<Guid> CreateNewReport();
        Task GenerateStatisticsReport(Guid reportId);
        Task<List<ReportDto>> GetAllReports();
        Task<ReportDetailDto> GetReportDetail(Guid reportId);
    }
}
