using Microsoft.EntityFrameworkCore;

namespace Report.API.Entities.Context
{
    public class ReportContext : DbContext
    {
        public ReportContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Report> Reports { get; set; }
    }
}
