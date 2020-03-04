using MoviesShop.DTO;
using MoviesShop.Models;
using System.Collections.Generic;

namespace MoviesShop.Mappers
{
    public static class CustomMapToRequestFilm
    {
        public static Film ConvertpToRequestFilm(this RequestFilmDTO @this)
        {
            //заполнение FilmActor
            List<FilmActor> filmActors = new List<FilmActor>();
            foreach (var item in @this.FilmActorDTO)
            {
                filmActors.Add(new FilmActor() { ActorId = item.SecondId });
            }

            //заполнение FilmGenre
            List<FilmGenre> filmGenres = new List<FilmGenre>();
            foreach (var item in @this.FilmGenreDTO)
            {
                filmGenres.Add(new FilmGenre() { GenreId = item.SecondId });
            }

            //заполнение Film
            Film film = new Film()
            {
                Id = @this.Id,
                Title = @this.Title,
                Year = @this.Year,
                Countrys = new Countrys() { NameOfTheCountry = @this.CountryDTO.CountryTitle },
                FilmActor = filmActors,
                FilmGenre = filmGenres
            };
            return film;
        }
    }
}
