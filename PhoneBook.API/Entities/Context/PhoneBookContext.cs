using Microsoft.EntityFrameworkCore;

namespace PhoneBook.API.Entities.Context
{
    public class PhoneBookContext : DbContext
    {
        public PhoneBookContext(DbContextOptions options) : base(options)
        {
        }

        protected PhoneBookContext()
        {
        }

        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<ContactInformation> ContactInformations { get; set; }
    }
}
