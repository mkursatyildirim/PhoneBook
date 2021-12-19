using PhoneBook.API.Entities.Context;

namespace PhoneBook.API.Services.Base
{
    public class BaseService
    {
        public readonly PhoneBookContext _context;

        public BaseService(PhoneBookContext context)
        {
            _context = context;
        }
    }
}
