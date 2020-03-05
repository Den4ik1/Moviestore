using MoviesShop.DTO;
using MoviesShop.Models;
using System.Collections.Generic;

namespace MoviesShop.Mappers
{
    public static class CustomMapToResponseFilm
    {
        public static ResponseFilmDTO ConvertToResponseFilm(this Film @this)
        {
            List<GenreDTO> genres = new List<GenreDTO>();

            foreach (var item in @this.FilmGenre)
            {
                genres.Add(new GenreDTO() { Title = item.Genre.Title });
            }

            List<ResponseActorDTO> actors = new List<ResponseActorDTO>();

            foreach (var item in @this.FilmActor)
            {
                actors.Add(new ResponseActorDTO() { Name = item.Actor.Name });
            }

            ResponseFilmDTO filmDTO = new ResponseFilmDTO()
            {
                Id = @this.Id,
                Title = @this.Title,
                Year = @this.Year,
                UrlImage = @this.UrlImage,
                CountryDTO = new CountryDTO() { CountryTitle = @this.Countrys.NameOfTheCountry },
                GenreDTO = genres,
                ActorDTO = actors
            };
            return filmDTO;
        }
    }
}
