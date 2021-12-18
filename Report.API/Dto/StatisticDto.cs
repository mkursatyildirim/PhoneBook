namespace Report.API.Dto
{
    public class StatisticDto
    {
        public Guid UUID { get; set; }
        public string Location { get; set; }
        public int PersonCount { get; set; }
        public int PhoneNumberCount { get; set; }
    }
}
