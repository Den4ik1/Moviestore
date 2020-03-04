using System.Collections.Generic;

namespace MoviesShop.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<FilmGenre> FilmGenre { get; set; }

        public Genre()
        {
            FilmGenre = new List<FilmGenre>();
        }
    }
}
