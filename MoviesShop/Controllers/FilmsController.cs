using Microsoft.AspNetCore.Mvc;
using MoviesShop.DTO;
using MoviesShop.Repository;
using System.Collections.Generic;
using System.Linq;
using MoviesShop.Mappers;

namespace MoviesShop.Controllers
{
    [Route("api/[controller]")]
    public class FilmsController
    {
        private readonly FilmRepository _repository;

        public FilmsController(FilmRepository repository)
        {
            _repository = repository;
        }

        //Вывод полной информации о (всех фильмах) / (по Id) / (Поиск по названию) /
        [HttpGet("[action]")]
        public List<ResponseFilmDTO> GetFilms([FromBody] RequestFilmDTO requestFilm)
        {
            List<ResponseFilmDTO> responseFilm = new List<ResponseFilmDTO>();

            if (requestFilm == null)
            {
                requestFilm = new RequestFilmDTO();
            }

            var film = _repository.GetFilm(requestFilm.ConvertpToRequestFilm()).ToList();

            foreach (var item in film)
            {
                responseFilm.Add(item.ConvertToResponseFilm());
            }

            return responseFilm;

            //Выборка по Id
            //if (requestFilm != null)
            //{
            //    if (requestFilm.Id > 0)
            //    {
            //        responseFilm.Add(_repository.GetForId(requestFilm.Id).ConvertToResponseFilm());
            //        return responseFilm;
            //    }
            //    //Выборка по названию
            //    else if (requestFilm.Title != null)
            //    {
            //        foreach (var item in _repository.GetFilmsForTitle(requestFilm.Title).ToList())
            //        {
            //            responseFilm.Add(item.ConvertToResponseFilm());
            //        };
            //    }
            //    //Выборка по жанру
            //    else if (requestFilm.FilmGenreDTO != null)
            //    {
            //        var t = requestFilm.FilmGenreDTO.FirstOrDefault(x => x > 0);
            //        foreach (var item in _repository.GetFilmsForGenre(requestFilm.FilmGenreDTO.First()))
            //        {
            //            responseFilm.Add(item.ConvertToResponseFilm());
            //        };
            //    }
            //}
            ////Все фильмы
            //else
            //{
            //    foreach (var item in _repository.GetFilms().ToList())
            //    {
            //        responseFilm.Add(item.ConvertToResponseFilm());
            //    }
            //}
            //return responseFilm;
        }

        //Вывод фильмов в которых снимался актёр
        [HttpGet("[action]")]
        public List<ResponseFilmDTO> GetFilmsForActor(RequestActorDTO actor)
        {
            List<ResponseFilmDTO> film = new List<ResponseFilmDTO>();
            foreach (var item in _repository.GetFilmsForActor(actor.Name).ToList())
            {
                film.Add(item.ConvertToResponseFilm());
            }
            return film;
        }

        // Создание/редактирование фильма
        [HttpPost("[action]")]
        public ResponseFilmDTO PostFilm([FromBody] RequestFilmDTO _film)
        {
            //List<int> tempActor = new List<int>(_film.FilmActorDTO);
            //_film.FilmActorDTO.Clear();
            ////!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //foreach (var item in tempActor)
            //{
            //    if (!_film.FilmActorDTO.Any(x => x == item))
            //    {
            //        _film.FilmActorDTO.Add(item);
            //    }
            //}

            ////Удаление дублекатов жанров во влеженном списке
            //List<int> tempGenre = new List<int>(_film.FilmGenreDTO);
            //_film.FilmGenreDTO.Clear();
            ////!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //foreach (var item in tempGenre)
            //{
            //    if (!_film.FilmGenreDTO.Any(x => x == item))
            //    {
            //        _film.FilmGenreDTO.Add(item);
            //    }
            //}

            //Передача данных в репозиторий
            if (_film.Id > 0)
            {
                return _repository.EditFilm(_film.ConvertpToRequestFilm()).ConvertToResponseFilm();
                //return _film;
            }
            return _repository.AddFilm(_film.ConvertpToRequestFilm()).ConvertToResponseFilm();
            //_repository.EditFilm(id, _film.ConvertpToRequestFilm());
        }

        //Удаление фильма по Id
        [HttpDelete("{id?}")]
        public void Delete(int? id)
        {
            _repository.DeleteFilm(id);
        }
    }
}