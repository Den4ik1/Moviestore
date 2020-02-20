using Microsoft.AspNetCore.Mvc;
using MoviesShop.DTO;
using MoviesShop.Repository;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

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
            return _mapper.Map<List<FilmDTO>>(_repository.GetFilms());
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

        // Создание/редактирование фильма
        [HttpPost("{Id?}")]
        public FilmDTO PostFilm(int? Id, [FromBody] FilmDTO _film)
        {
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