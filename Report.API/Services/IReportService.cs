namespace Report.API.Services
{
    public interface IReportService
    {
        Task GenerateStatisticsReport(Guid reportId);
    }
}
