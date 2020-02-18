using MoviesShop.DTO;
using MoviesShop.Models;

namespace MoviesShop.Mappers
{
    static public class CustomMapToFilmDTO
    {
        public static FilmDTO ConvertToFilmDTO(this Film @this)
        {

            //заполнение FilmActor
            //List<FilmActor> fa = new List<FilmActor>() { };
            //foreach (var item in @this.FilmsActorDTO)
            //{
            //    fa.Add(new FilmActor() { ActorId = item.ActorId, FilmId = item.FilmId });
            //}

            //заполениен Actor
            FilmDTO result = new FilmDTO()
            {
                Id = @this.Id,
                Title = @this.Title,
                Year = @this.Year,
                CountryDTO = new TitleDTO() { Title = @this.Countrys.NameOfTheCountry },
                // FilmActor = fa,
            };


            return result;

        }
    }
}
