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
        public List<FilmDTO> GetFilms(int? id)
        {
            List<FilmDTO> film = new List<FilmDTO>();

            if (id.HasValue)
            {
                film.Add(_repository.GetForId(id).ConvertToFilmDTO());
                return film;
            }
            else
            {
                foreach (var item in _repository.GetFilms().ToList())
                {
                    film.Add(item.ConvertToFilmDTO());
                }
            }
            return film;
        }

        //Поиск по названию
        [HttpGet("Title/{title}")]
        public List<FilmDTO> GetFilmstitle(string title)
        {
            List<FilmDTO> film = new List<FilmDTO>();

            foreach (var item in _repository.GetFilmsForTitle(title).ToList())
            {
                film.Add(item.ConvertToFilmDTO());
            };
            return film;
        }

        //Выборка по жанру
        [HttpGet("genre/{genre}")]
        public List<FilmDTO> GetFilmGanre(string genre)
        {
            List<FilmDTO> film = new List<FilmDTO>();

            foreach (var item in _repository.GetFilmsForGenre(genre).ToList())
            {
                film.Add(item.ConvertToFilmDTO());
            };
            return film;
        }

        //Вывод фильмов в которых снимался актёр
        [HttpGet("[action]")]
        public List<FilmDTO> GetFilmActor(string actor)
        {
            List<FilmDTO> film = new List<FilmDTO>();
            foreach (var item in _repository.GetFilmsForActor(actor).ToList())
            {
                film.Add(item.ConvertToFilmDTO());
            }
            return film;
        }

        // Создание/редактирование фильма
        [HttpPost("{id?}")]
        public FilmDTO PostFilm(int? id, [FromBody] FilmDTO _film)
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
                _repository.AddFilm(_film);
                return _film;
            }
            _repository.EditFilm(id, _film);
            return _film;
        }

        //Удаление фильма по Id
        [HttpDelete("{id?}")]
        public void Delete(int? id)
        {
            _repository.DeleteFilm(id);
        }
    }
}