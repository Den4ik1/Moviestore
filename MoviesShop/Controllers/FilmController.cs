using Microsoft.AspNetCore.Mvc;
using MoviesShop.DTO;
using MoviesShop.Repository;
using System.Collections.Generic;
using System.Linq;
using MoviesShop.Mappers;

namespace MoviesShop.Controllers
{
    [Route("api/[controller]")]
    public class FilmController
    {
        private readonly FilmRepository _repository;

        public FilmController(FilmRepository repository)
        {
            _repository = repository;
        }

        //Вывод полной информации о (всех фильмах) / (по Id)
        [HttpGet("{id?}")]
        public List<ResponseFilmDTO> GetFilms(int? id)
        {
            List<ResponseFilmDTO> film = new List<ResponseFilmDTO>();

            if (id.HasValue)
            {
                film.Add(_repository.GetForId(id).ConvertToResponseFilm());
                return film;
            }
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
        [HttpGet("genre/{genre}")]
        public List<ResponseFilmDTO> GetFilmGanre(string genre)
        {
            List<ResponseFilmDTO> film = new List<ResponseFilmDTO>();

            foreach (var item in _repository.GetFilmsForGenre(genre).ToList())
            {
                film.Add(item.ConvertToResponseFilm());
            };
            return film;
        }

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
        [HttpPost("{id?}")]
        public ResponseFilmDTO PostFilm(int? id, [FromBody] RequestFilmDTO _film)
        {
            List<RelationshipStagingDTO> tempActor = new List<RelationshipStagingDTO>(_film.FilmActorDTO);
            _film.FilmActorDTO.Clear();
            foreach (var item in tempActor)
            {
                if (!_film.FilmActorDTO.Any(x => x.SecondId == item.SecondId))
                {
                    _film.FilmActorDTO.Add(item);
                }
            }

            //Удаление дублекатов жанров во влеженном списке
            List<RelationshipStagingDTO> tempGenre = new List<RelationshipStagingDTO>(_film.FilmGenreDTO);
            _film.FilmGenreDTO.Clear();
            foreach (var item in tempGenre)
            {
                if (!_film.FilmGenreDTO.Any(x => x.SecondId == item.SecondId))
                {
                    _film.FilmGenreDTO.Add(item);
                }
            }

            //Передача данных в репозиторий
            if (id == 0)
            {
                return _repository.AddFilm(_film.ConvertpToRequestFilm()).ConvertToResponseFilm();
                //return _film;
            }
            //_repository.EditFilm(id, _film.ConvertpToRequestFilm());
            return _repository.EditFilm(id, _film.ConvertpToRequestFilm()).ConvertToResponseFilm();
        }

        //Удаление фильма по Id
        [HttpDelete("{id?}")]
        public void Delete(int? id)
        {
            _repository.DeleteFilm(id);
        }
    }
}