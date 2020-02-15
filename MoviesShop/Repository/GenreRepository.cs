using MoviesShop.Models;
using System.Linq;

namespace MoviesShop.Repository
{
    public class GenreRepository
    {
        private readonly MoviesShopContext _context;

        public GenreRepository(MoviesShopContext context)
        {
            _context = context;
        }

        public IQueryable<Genre> GetGenres()
        {
            return _context.Set<Genre>();
        }
    }
}
