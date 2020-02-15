using Microsoft.EntityFrameworkCore;

namespace MoviesShop.Models
{
    public class MoviesShopContext : DbContext
    {
        public MoviesShopContext (DbContextOptions<MoviesShopContext> options)
            : base(options)
        {
        }

        public DbSet<Genre> Genre { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserFilm> UserFilm { get; set; }
        public DbSet<FilmGenre> FilmGenre { get; set; }
        public DbSet<Film> Film { get; set; }
        public DbSet<Countrys> Country { get; set; }
        public DbSet<FilmActor> FilmActor { get; set; }
        public DbSet<Actor> Actor { get; set; }
    }
}
