using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Report.API.Constants;
using Report.API.Dto;
using Report.API.Entities;
using Report.API.Entities.Context;
using Report.API.Enums;
using Entites = Report.API.Entities;
namespace Report.API.Services
{
    public class ReportService : IReportService
    {
        private readonly ReportContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ReportService> _logger;
        private readonly ReportSettings _reportSettings;

        public ReportService(ReportContext context, IHttpClientFactory httpClientFactory, ILogger<ReportService> logger, IOptions<ReportSettings> reportSettings)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _reportSettings = reportSettings?.Value;
        }

        public async Task GenerateStatisticsReport(Guid reportId)
        {
            var report = await _context.Reports.Where(x => x.UUID == reportId).FirstOrDefaultAsync();

            if (report == null)
            {
                _logger.LogError("Rapor bulunamadı.");
                throw new Exception();
            }

            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_reportSettings.PhoneBookApiUrl}/Persons/ContactInformations");
            var response = await client.SendAsync(request);

            var responseStream = await response.Content.ReadAsStringAsync();
            var contactInformations = JsonConvert.DeserializeObject<IEnumerable<ContactInformationDto>>(responseStream);

            var statisticsReport = contactInformations.Where(x => x.InformationType == 2).Select(x => x.InformationContent).Distinct().Select(x => new ReportDetail
            {
                ReportId = reportId,
                Location = x,
                PersonCount = contactInformations.Where(y => y.InformationType == 2 && y.InformationContent == x).Count(),
                PhoneNumberCount = contactInformations.Where(y => y.InformationType == 0 && contactInformations.Where(y => y.InformationType == 2 && y.InformationContent == x).Select(x => x.PersonId).Contains(y.PersonId)).Count()
            });

            report.ReportStatus = ReportStatus.Completed;

            await _context.ReportDetails.AddRangeAsync(statisticsReport);
            await _context.SaveChangesAsync();
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

        public async Task<List<ReportDto>> GetAllReports()
        {
            var reports = await _context.Reports.Select(r => new ReportDto()
            {
                UUID = r.UUID,
                ReportStatus = r.ReportStatus,
                RequestDate = r.RequestDate
            }).ToListAsync();

            return reports;
        }

        public async Task<ReportDetailDto> GetReportDetail(Guid reportId)
        {
            var reportDetails = await _context.Reports.Where(r => r.UUID == reportId).Select(r =>
             new ReportDetailDto()
             {
                 Report = new ReportDto()
                 {
                     UUID = reportId,
                     ReportStatus = r.ReportStatus,
                     RequestDate = r.RequestDate
                 },
                 ReportDetails = r.ReportDetails.Select(rd => new StatisticDto()
                 {
                     UUID = rd.UUID,
                     Location = rd.Location,
                     PersonCount = rd.PersonCount,
                     PhoneNumberCount = rd.PhoneNumberCount
                 }).ToList()
             }).FirstOrDefaultAsync();

            return reportDetails;
        }
    }
}
