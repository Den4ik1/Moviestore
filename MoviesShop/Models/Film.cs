using MoviesShop.Data;
using System.Collections.Generic;

namespace MoviesShop.Models
{
    public class Film : IData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UrlImage { get; set; }
        public int Year { get; set; }
        public Countrys Countrys { get; set; }

        public ICollection<FilmActor> FilmActor { get; set; }

        public ICollection<FilmGenre> FilmGenre { get; set; }

        public ICollection<UserFilm> UserFilm { get; set; }

        public Film()
        {
            UserFilm = new List<UserFilm>();
            FilmGenre = new List<FilmGenre>();
            FilmActor = new List<FilmActor>();
        }

       
    }
}
