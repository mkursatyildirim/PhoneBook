using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Report.API.Constants;
using Report.API.Dto;
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
            _reportSettings = reportSettings.Value;
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

            var statisticsReport = contactInformations.Where(x => x.InformationType == 2).Select(x => x.InformationContent).Distinct().Select(x => new
            {
                Location = x,
                TotalPersonCount = contactInformations.Where(y => y.InformationType == 2 && y.InformationContent == x).Count(),
                TotalPhoneNumberCount = contactInformations.Where(y => y.InformationType == 0 && contactInformations.Where(y => y.InformationType == 2 && y.InformationContent == x).Select(x => x.PersonId).Contains(y.PersonId)).Count()
            });

            report.ReportStatus = ReportStatus.Completed;
            // ToDo: Detaylar oluşturulacak.
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
    }
}
