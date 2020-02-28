using MoviesShop.DTO;
using MoviesShop.Models;
using System.Collections.Generic;

namespace MoviesShop.Mappers
{
    public static class CustommapToFillmDTO
    {
        public static FilmDTO ConvertToFilmDTO(this Film @this)
        {
            List<GenreDTO> genres = new List<GenreDTO>();

            foreach (var item in @this.FilmGenre)
            {
                genres.Add(new GenreDTO() {Title = item.Genre.Title});
            }

            List<ActorDTO> actors = new List<ActorDTO>();

            foreach (var item in @this.FilmActor)
            {
                actors.Add(new ActorDTO() {Name = item.Actor.Name});
            }

            FilmDTO filmDTO = new FilmDTO()
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
