using System.ComponentModel;

namespace Report.API.Enums
{
    public enum ReportStatus
    {
        [Description("Hazırlanıyor")]
        Preparing,
        [Description("Tamamlandi")]
        Completed
    }
}
