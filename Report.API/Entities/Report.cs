using Report.API.Entities.Base;
using Report.API.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Report.API.Entities
{
    [Table("reports")]
    public class Report : BaseEntity
    {
        public DateTime RequestDate { get; set; }
        public ReportStatus ReportStatus { get; set; }
        public virtual ReportDetail ReportDetail { get; set; }
    }
}
