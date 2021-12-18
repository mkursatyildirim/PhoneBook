namespace Report.API.Dto
{
    public class ReportDetailDto
    {
        public ReportDto Report { get; set; }
        public List<StatisticDto> ReportDetails { get; set; }
    }
}
