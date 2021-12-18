namespace Report.API.Services
{
    public interface IReportService
    {
        Task<Guid> CreateNewReport();
        Task GenerateStatisticsReport(Guid reportId);
    }
}
