using Microsoft.EntityFrameworkCore;
using MoviesShop.DTO;
using MoviesShop.Mappers;
using MoviesShop.Models;
using System.Collections.Generic;
using System.Linq;

namespace MoviesShop.Repository
{
    public class FilmRepository 
    {
        private readonly MoviesShopContext _context;
        private delegate Countrys _сheckConunty(string titleCounty);
        private _сheckConunty _testConunty;

        public FilmRepository(MoviesShopContext context)
        {
            _context = context;
            _testConunty = new ExistCountry(context).TestCountry;
        }

        //Вывод всех фильмов и доп. информацию (жанры, страна произвотсва и т.д.)
        public IQueryable<Film> GetFilms()
        {
            var temp = _context.Film
                .Include(af => af.FilmActor).ThenInclude(a => a.Actor)
                .Include(gf => gf.FilmGenre).ThenInclude(g => g.Genre)
                .Include(c => c.Countrys);
            var r = temp.ToList();

            return temp;
        }

        //Поиск по Id
        public Film GetId(int? Id)
        {
            var result = _context.Film.Include(af => af.FilmActor).ThenInclude(a => a.Actor)
                 .Include(gf => gf.FilmGenre).ThenInclude(g => g.Genre)
                 .Include(c => c.Countrys)
                 .First(f => f.Id == Id);

            if (result != null)
            {
                return result;
            }
            return new Film();
        }

        //Поиск по названию
        public IQueryable<Film> GetFilmsTitle(string title)
        {
            var result = _context.Film.Include(af => af.FilmActor).ThenInclude(a => a.Actor)
               .Include(gf => gf.FilmGenre).ThenInclude(g => g.Genre)
               .Include(c => c.Countrys)
               .Where(p => p.Title.Contains($"{title}"));
            return result;
        }

        //Фильтрация по жанрам
        public IQueryable<Film> GetFilmsGenre(string genre)
        {
            var gen = _context.Genre.Include(g => g.FilmGenre).FirstOrDefault(g => g.Title.Contains(genre));
            var ids = gen.FilmGenre.Select(fg => fg.FilmId).ToList();
            var films = _context.Film.Where(f => ids.Contains(f.Id))
                .Include(c => c.Countrys)
                .Include(af => af.FilmActor).ThenInclude(a => a.Actor)
                .Include(gf => gf.FilmGenre).ThenInclude(g => g.Genre);

            return films;
        }

        //Добавление нового фильма
        public void AddFilm(FilmDTO _film)
        {
            var FilmsDBQ = _context.Film;
            var FilmsDB = FilmsDBQ.ToList();

            var newFilm = _film.ConvertToFilme();

            if (!FilmsDB.Any(x =>
             (x.Title == newFilm.Title) &&
             (x.Countrys.NameOfTheCountry == newFilm.Countrys.NameOfTheCountry) &&
             (x.Year == newFilm.Year)))
            {
                _context.Film.Add(newFilm);
            }
            _context.SaveChanges();


            #region
            List<FilmActor> filmActor = new List<FilmActor>();

            foreach (var item in newFilm.FilmActor)
            {
                if (!filmActor.Any(x => x.ActorId == item.ActorId))
                {
                    FilmActor temp = new FilmActor() { FilmId = item.FilmId, ActorId = item.ActorId };
                    filmActor.Add(temp);
                }
            }
            _context.SaveChanges();
            #endregion
            
            #region
            List<FilmGenre> filmGenre = new List<FilmGenre>();

            foreach (var item in newFilm.FilmGenre)
            {
                if (!filmGenre.Any(x => x.GenreId == item.GenreId))
                {
                    FilmGenre temp = new FilmGenre() { FilmId = item.FilmId, GenreId = item.GenreId};
                    filmGenre.Add(temp);
                }
            }
            _context.SaveChanges();
            #endregion
        }

        //Редактирование фильма
        public void EditFilm(int? Id, FilmDTO _film)
        {
            int idf = (int)Id;

            Film film = new Film();
            film = _film.ConvertToFilme();

            var EditFilm = _context.Film.First(x => x.Id == idf);
            EditFilm.Title = _film.Title;
            EditFilm.Year = _film.Year;
            EditFilm.UrlImage = _film.UrlImage;
            EditFilm.Countrys = _testConunty(_film.CountryDTO.Title);
           //_context.SaveChanges();
            #region Добавление Актёров

            //удаление дубликатов актёров, в данных полученных от пользователя
            List<FilmActor> filmActor = new List<FilmActor>();
            foreach (var item in film.FilmActor)
            {
                if (!filmActor.Any(x => x.ActorId == item.ActorId))
                {
                    item.FilmId = idf;
                    filmActor.Add(item);
                }
            }

            film.FilmActor = new List<FilmActor>();
            
            //проверка если такие фильмы у актёра в базе.
            //собираем всех актёров которые снимальси в этом фильме
            IQueryable<FilmActor> filmActorsDBQ = _context.FilmActor.Where(x => x.FilmId == idf);
            List<FilmActor> filmActorsDB = filmActorsDBQ.ToList();

            foreach (var item in filmActorsDB)
            {
                if (filmActor.Any(x => x.ActorId == item.ActorId))
                {
                    filmActor.Remove(filmActor.First(x => x.ActorId == item.ActorId));
                }
            }

            foreach (var item in filmActor)
            {
                _context.FilmActor.Add(item);
            }
            #endregion
            //_context.SaveChanges();
            #region Добавление Жанров

            //удаление дубликатов жанров, в данных полученных от пользователя
            List<FilmGenre> filmGenre = new List<FilmGenre>();
            foreach (var item in film.FilmGenre)
            {
                if (!filmGenre.Any(x => x.GenreId == item.GenreId))
                {
                    item.FilmId = idf;
                    filmGenre.Add(item);
                }
            }
            
            film.FilmGenre = new List<FilmGenre>();
            //проверка если такие фильмы у актёра в базе.
            //собираем все жанры в которых снят фильм
            IQueryable<FilmGenre> genreDBQ = _context.FilmGenre.Where(x => x.FilmId == idf);
            List<FilmGenre> genreDB = genreDBQ.ToList();

            foreach (var item in genreDB)
            {
                if (filmGenre.Any(x => x.GenreId == item.GenreId))
                {
                    filmGenre.Remove(filmGenre.First(x => x.GenreId == item.GenreId));
                }
               
            }

            foreach (var item in filmGenre)
            {
                _context.FilmGenre.Add(item);
            }
            #endregion

            _context.SaveChanges();

        }
       
        //Удаление по Id
        public void DeleteFilm(int? Id)
        {
            _context.Film.Remove(_context.Film.First(x => x.Id == Id));
            _context.SaveChanges();
        }
    }
}



