using Report.API.Enums;
using Report.API.Helpers;

namespace Report.API.Dto
{
    public class ReportDto
    {
        public Guid UUID { get; set; }
        public DateTime RequestDate { get; set; }
        public ReportStatus ReportStatus { get; set; }
        public string ReportStatusDescription => EnumHelper.GetDescription(ReportStatus);
    }
}
