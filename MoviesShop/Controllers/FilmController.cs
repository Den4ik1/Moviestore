using Microsoft.AspNetCore.Mvc;
using MoviesShop.DTO;
using MoviesShop.Repository;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MoviesShop.Mappers;

namespace MoviesShop.Controllers
{
    [Route("api/[controller]")]
    public class FilmController
    {
        private readonly FilmRepository _repository;
        private readonly IMapper _mapper;

        public FilmController(FilmRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //Вывод полной информации о фсех фильмах
        [HttpGet("{Id?}")]
        public List<FilmDTO> getFilms(int? Id)
        {
            if (Id.HasValue)
            {
               return new List<FilmDTO>() { _mapper.Map<FilmDTO>(_repository.GetId(Id)) };
               // return _mapper.Map<List<FilmDTO>>(new List<Film>() { _repository.GetId(Id) });
            }
            //List<FilmDTO> r = new List<FilmDTO>();
            //r =_repository.GetFilms().ToList().ConvertToFilmDTO();
            //var r = _mapper.Map<List<FilmDTO>>(_repository.GetFilms());
            List<FilmDTO> actor = new List<FilmDTO>();
            foreach (var item in _repository.GetFilms().ToList())
            {
                actor.Add(item.ConvertToFilmDTO());
            }
            return actor;
        }

        //Поиск по названию
        [HttpGet("Title/{title}")]
        public List<FilmDTO> getFilmstitle(string title)
        {
            return _mapper.Map<List<FilmDTO>>(_repository.GetFilmsTitle(title).ToList());
        }

        //Фильтрация по жанру
        [HttpGet("genre/{genre}")]
        public List<FilmDTO> GetFilmGanre(string genre)
        {
            return _mapper.Map<List<FilmDTO>>(_repository.GetFilmsGenre(genre).ToList());
        }

        //Вывод фильмов в которых снимался актёр

        // Создание/редактирование фильма
        [HttpPost("{Id?}")]
        public FilmDTO PostFilm(int? Id, [FromBody] FilmDTO _film)
        {
            //Удаление дублекатов актёров во влеженном списке
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
            if (Id == 0)
            {
                _repository.AddFilm(_film);
                return _film;
            }
            _repository.EditFilm(Id, _film);
            return _film;
        }

        //Удаление фильма по Id
        [HttpDelete("{Id?}")]
        public void Delete(int? Id)
        {
            _repository.DeleteFilm(Id);
        }
    }
}