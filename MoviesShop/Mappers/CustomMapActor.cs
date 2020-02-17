//using MoviesShop.DTO;
//using MoviesShop.Models;
//using System.Collections.Generic;

//namespace MoviesShop.Mappers
//{
//    static public class CustomMapActor
//    {
//        static public Actor ConvertToActor(ActorDTO _actorDTO)
//        {

//            ////////////////////////////////////////////////////////////
//            List<string> filmsListString = new List<string>();
           

//            List<Film> films = new List<Film>();


//            var ac = new Actor()
//            {
//                Id = _actorDTO.Id,
//                Name = _actorDTO.Name,
//                Country = new Countrys() { NameOfTheCountry = _actorDTO.CountryDTO.Title },
//                BirthDay = _actorDTO.BirthDay,
//            };

//            foreach (string item in _actorDTO.Films)
//            {

//                FilmActor filmA = new FilmActor();

//                var temp = item.Split(' ');


//                var year = temp[temp.Length - 2];
//                var country = temp[temp.Length - 1];
//                var film = new Film()
//                {
//                    Title = item.Remove(item.Length - year.Length - country.Length - 1),
//                    Year = int.Parse(year),
//                    Countrys = new Countrys() { NameOfTheCountry = country }
//                };
//                films.Add(film);
//                filmA.Film = film;
//                filmA.Actor = ac;
//            }

//            return new Actor()
//            {
//                Id = _actorDTO.Id,
//                Name = _actorDTO.Name,
//                Country = new Countrys() { NameOfTheCountry = _actorDTO.CountryDTO.Title },
//                BirthDay = _actorDTO.BirthDay,
//            };

//            ////////////////////////////////////////////////////////////
//            //var films = new List<Film>();
//            //foreach (var item in _actorDTO.Films)
//            //{
//            //    films.Add(new Film() { Id = item });
//            //}

//            //var actor = new Actor()
//            //{
//            //    Id = _actorDTO.Id,
//            //    Name = _actorDTO.Name,
//            //    BirthDay = _actorDTO.BirthDay,
//            //    Country = new Countrys() { NameOfTheCountry = _actorDTO.CountryDTO.Title },
//            //    FilmActor = null,
//            //};
//            //return (actor);
                
//        }
//    }
//}
