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
        [HttpGet("{id?}")]
        public List<ResponseFilmDTO> GetFilms([FromBody] RequestFilmDTO _film)
        {
            List<ResponseFilmDTO> film = new List<ResponseFilmDTO>();
            //Выборка по Id
            if (_film != null)
            {
                if (_film.Id > 0)
                {
                    film.Add(_repository.GetForId(_film.Id).ConvertToResponseFilm());
                    return film;
                }
                //Выборка по названию
                else if (_film.Title != null || _film.Title != "")
                {
                    foreach (var item in _repository.GetFilmsForTitle(_film.Title).ToList())
                    {
                        film.Add(item.ConvertToResponseFilm());
                    };
                }
                //Выборка по жанру
                else if (_film.FilmGenreDTO != null)
                {
                    var t = _film.FilmGenreDTO.FirstOrDefault(x => x > 0);
                    foreach (var item in _repository.GetFilmsForGenre(_film.FilmGenreDTO.First()))
                    {
                        film.Add(item.ConvertToResponseFilm());
                    };
                }
            }
            //Все фильмы
            else
            {
                foreach (var item in _repository.GetFilms().ToList())
                {
                    film.Add(item.ConvertToResponseFilm());
                }
            }
            return film;
        }

        //Поиск по названию
        [HttpGet("Title/{title}")]
        public List<ResponseFilmDTO> GetFilmstitle(string title)
        {
            List<ResponseFilmDTO> film = new List<ResponseFilmDTO>();

            foreach (var item in _repository.GetFilmsForTitle(title).ToList())
            {
                film.Add(item.ConvertToResponseFilm());
            };
            return film;
        }

        //Выборка по жанру
        //[HttpGet("genre/{genre}")]
        //public List<ResponseFilmDTO> GetFilmGanre(string genre)
        //{
        //    List<ResponseFilmDTO> film = new List<ResponseFilmDTO>();

        //    foreach (var item in _repository.GetFilmsForGenre(genre).ToList())
        //    {
        //        film.Add(item.ConvertToResponseFilm());
        //    };
        //    return film;
        //}

        //Вывод фильмов в которых снимался актёр
        [HttpGet("[action]")]
        public List<ResponseFilmDTO> GetFilmActor(string actor)
        {
            List<ResponseFilmDTO> film = new List<ResponseFilmDTO>();
            foreach (var item in _repository.GetFilmsForActor(actor).ToList())
            {
                film.Add(item.ConvertToResponseFilm());
            }
            return film;
        }

        // Создание/редактирование фильма
        [HttpPost]
        public ResponseFilmDTO PostFilm([FromBody] RequestFilmDTO _film)
        {
            List<int> tempActor = new List<int>(_film.FilmActorDTO);
            _film.FilmActorDTO.Clear();
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            foreach (var item in tempActor)
            {
                if (!_film.FilmActorDTO.Any(x => x == item))
                {
                    _film.FilmActorDTO.Add(item);
                }
            }

            //Удаление дублекатов жанров во влеженном списке
            List<int> tempGenre = new List<int>(_film.FilmGenreDTO);
            _film.FilmGenreDTO.Clear();
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            foreach (var item in tempGenre)
            {
                if (!_film.FilmGenreDTO.Any(x => x == item))
                {
                    _film.FilmGenreDTO.Add(item);
                }
            }

            //Передача данных в репозиторий
            if (_film.Id == 0)
            {
                return _repository.AddFilm(_film.ConvertpToRequestFilm()).ConvertToResponseFilm();
                //return _film;
            }
            //_repository.EditFilm(id, _film.ConvertpToRequestFilm());
            return _repository.EditFilm(_film.Id, _film.ConvertpToRequestFilm()).ConvertToResponseFilm();
        }

        //Удаление фильма по Id
        [HttpDelete("{id?}")]
        public void Delete(int? id)
        {
            _repository.DeleteFilm(id);
        }
    }
}