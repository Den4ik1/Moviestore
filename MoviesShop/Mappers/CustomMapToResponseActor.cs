using MoviesShop.DTO;
using MoviesShop.Models;
using System.Collections.Generic;

namespace MoviesShop.Mappers
{
    public static class CustomMapToResponseActor
    {
        public static ResponseActorDTO ConvertToResponseActor(this Actor @this)
        {
            List<ResponseFilmDTO> films = new List<ResponseFilmDTO>();

            foreach (var item in @this.FilmActor)
            {
                films.Add(new ResponseFilmDTO()
                {
                    Id = item.FilmId,
                    Title = item.Film.Title,
                    Year = item.Film.Year
                });
            }

            ResponseActorDTO actor = new ResponseActorDTO()
            {
                Id = @this.Id,
                Name = @this.Name,
                BirthDay = @this.BirthDay,
                CountryDTO = new CountryDTO() { CountryTitle = @this.Country.NameOfTheCountry },
                FilmsDTO = films
            };

            return actor;
        }
    }
}
