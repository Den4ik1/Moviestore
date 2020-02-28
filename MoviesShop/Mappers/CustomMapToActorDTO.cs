using MoviesShop.DTO;
using MoviesShop.Models;
using System.Collections.Generic;

namespace MoviesShop.Mappers
{
    public static class CustomMapToActorDTO
    {
        public static ActorDTO ConvertToActorDTO(this Actor @this)
        {
            List<FilmDTO> films = new List<FilmDTO>();

            foreach (var item in @this.FilmActor)
            {
                films.Add(new FilmDTO()
                {
                    Id = item.Id,
                    Title = item.Film.Title,
                    Year = item.Film.Year
                });
            }

            ActorDTO actor = new ActorDTO()
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
