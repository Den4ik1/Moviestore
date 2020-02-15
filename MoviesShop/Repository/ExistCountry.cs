using MoviesShop.Models;
using System.Linq;

namespace MoviesShop.Repository
{
    public class ExistCountry
    {
        private readonly MoviesShopContext _context;

        public ExistCountry(MoviesShopContext context)
        {
            _context = context;
        }

        public Countrys TestCountry(string _title)
        {
            //Проверка, есть ли такая страна в Базе
            if (_context.Country.Any(x => x.NameOfTheCountry == _title))
            {
                return _context.Country.First(x => x.NameOfTheCountry == _title);
            }
            else
            {
                return new Countrys() { NameOfTheCountry = _title };
            }
        }
    }
}
