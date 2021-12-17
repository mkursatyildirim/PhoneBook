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
    }
}
