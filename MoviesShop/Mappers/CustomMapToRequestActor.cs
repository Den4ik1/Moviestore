using MoviesShop.DTO;
using MoviesShop.Models;
using System.Collections.Generic;

namespace MoviesShop.Mappers
{
    public static class CustomMapToRequestActor
    {
        public static Actor ConvertToActor(this RequestActorDTO @this)
        {
            //заполнение FilmActor
            List<FilmActor> filmActor = new List<FilmActor>();
            foreach (var item in @this.FilmsActorDTO)
            {
                filmActor.Add(new FilmActor() { ActorId = @this.Id, FilmId = item });
            }

            if (@this.CountryDTO == null)
            {
                @this.CountryDTO = new CountryDTO();
            }

            //заполнение Actor
            Actor result = new Actor()
            {
                Id = @this.Id,
                Name = @this.Name,
                Country = new Countrys() { NameOfTheCountry = @this.CountryDTO.CountryTitle },
                BirthDay = @this.BirthDay,
                FilmActor = filmActor,
            };
            return result;
        }
    }
}
