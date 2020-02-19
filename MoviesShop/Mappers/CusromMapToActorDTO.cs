using MoviesShop.DTO;
using MoviesShop.Models;
using System.Collections.Generic;
using System.Linq;

namespace MoviesShop.Mappers
{
    public static class CustomMapToActorDTO
    {
        public static ActorDTO ConvertToActorDTO(this Actor @this)
        {
            //заполнение FilmActor
            List<Film> films = new List<Film>();

            films = @this.FilmActor.Select(x => x.Film).ToList();

            List<TitleDTO> filmDTO = new List<TitleDTO>();

            foreach (var item in films)
            {
                filmDTO.Add(new TitleDTO() { Id = item.Id, Title = item.Title });
            }

            //fa.Select(x => x.ConvertToFilmDTO()).ToList();


            //foreach (var item in @this.FilmsActorDTO)
            //{
            //    fa.Add(new FilmActor() { ActorId = item.ActorId, FilmId = item.FilmId });
            //}

            //заполениен Actor
            ActorDTO result = new ActorDTO()
            {
                Id = @this.Id,
                Name = @this.Name,
                CountryDTO = new TitleDTO() { Title = @this.Country.NameOfTheCountry },
                BirthDay = @this.BirthDay,
                FilmsDTO = filmDTO
            };


            return result;
            
        }
    }
}