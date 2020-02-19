using MoviesShop.DTO;
using MoviesShop.Models;
using System.Collections.Generic;

namespace MoviesShop.Mappers
{
    public static class CustomMapToActor
    {
        public static Actor ConvertToActor(this ActorDTO @this)
        {
            //заполнение FilmActor
            List<FilmActor> filmActor = new List<FilmActor>();
            foreach (var item in @this.FilmsActorDTO)
            {
                filmActor.Add(new FilmActor() { ActorId = item.MainId, FilmId = item.SecondId });
            }

            //заполениен Actor
            Actor result = new Actor()
            {
                Id = @this.Id,
                Name = @this.Name,
                Country = new Countrys() { NameOfTheCountry = @this.CountryDTO.Title },
                BirthDay = @this.BirthDay,
                FilmActor = filmActor,
            };
            return result;
        }
    }
}
