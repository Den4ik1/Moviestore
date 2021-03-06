﻿using Microsoft.EntityFrameworkCore;
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
        private readonly _сheckConunty _testConunty;

        public FilmRepository(MoviesShopContext context)
        {
            _context = context;
            _testConunty = new ExistCountry(context).TestCountry;
        }

        //Вывод всех фильмов и доп. информацию (жанры, страна произвотсва и т.д.)
        public IQueryable<Film> GetFilms()
        {
            var result = _context.Film
                .Include(af => af.FilmActor).ThenInclude(a => a.Actor)
                .Include(gf => gf.FilmGenre).ThenInclude(g => g.Genre)
                .Include(c => c.Countrys);
            return result;
        }

        //Поиск по Id
        public Film GetForId(int? id)
        {
            var result = _context.Film.Include(af => af.FilmActor).ThenInclude(a => a.Actor)
                 .Include(gf => gf.FilmGenre).ThenInclude(g => g.Genre)
                 .Include(c => c.Countrys)
                 .First(f => f.Id == id);

            if (result != null)
            {
                return result;
            }
            return new Film();
        }

        //Поиск по названию
        public IQueryable<Film> GetFilmsForTitle(string title)
        {
            var result = _context.Film.Include(af => af.FilmActor).ThenInclude(a => a.Actor)
               .Include(gf => gf.FilmGenre).ThenInclude(g => g.Genre)
               .Include(c => c.Countrys)
               .Where(p => p.Title.Contains($"{title}"));
            return result;
        }

        //Выборка по жанрам
        public IQueryable<Film> GetFilmsForGenre(string genre)
        {
            //Вывод Id жанра по токотору совершена выборка
            var gen = _context.Genre.Include(g => g.FilmGenre).FirstOrDefault(g => g.Title.Contains(genre));
            var idg = gen.FilmGenre.Select(fg => fg.FilmId).ToList();

            var films = _context.Film.Where(f => idg.Contains(f.Id))
                .Include(c => c.Countrys)
                .Include(af => af.FilmActor).ThenInclude(a => a.Actor)
                .Include(gf => gf.FilmGenre).ThenInclude(g => g.Genre);
            return films;
        }

        //Выборка по актёру
        public IQueryable<Film> GetFilmsForActor(string actor)
        {
            //Вывод Id актйра по токотору совершена выборка
            var act = _context.Actor.Include(a => a.FilmActor).FirstOrDefault(a => a.Name.Contains(actor));
            var ida = act.FilmActor.Select(fa => fa.FilmId).ToList();


            var films = _context.Film.Where(f => ida.Contains(f.Id))
                .Include(c => c.Countrys)
                .Include(af => af.FilmActor).ThenInclude(a => a.Actor)
                .Include(gf => gf.FilmGenre).ThenInclude(g => g.Genre);
            return films;
        }

        //Добавление нового фильма
        public Film AddFilm(Film film)
        {
            var FilmsDBQ = _context.Film;
            var FilmsDB = FilmsDBQ.ToList();

            //var newFilm = _film.ConvertToFilme();

            if (!FilmsDB.Any(x =>
             (x.Title == film.Title) &&
             (x.Countrys.NameOfTheCountry == film.Countrys.NameOfTheCountry) &&
             (x.Year == film.Year)))
            {
                _context.Film.Add(film);
            }
            _context.SaveChanges();


            #region
            List<FilmActor> filmActor = new List<FilmActor>();

            foreach (var item in film.FilmActor)
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

            foreach (var item in film.FilmGenre)
            {
                if (!filmGenre.Any(x => x.GenreId == item.GenreId))
                {
                    FilmGenre temp = new FilmGenre() { FilmId = item.FilmId, GenreId = item.GenreId};
                    filmGenre.Add(temp);
                }
            }
            _context.SaveChanges();
            #endregion
            return film;
        }

        //Редактирование фильма
        public Film EditFilm(int? id, Film film)
        {
            int idf = (int)id;

            //Film film = new Film();
            //film = _film.ConvertToFilme();

            var EditFilm = _context.Film.First(x => x.Id == idf);
            EditFilm.Title = film.Title;
            EditFilm.Year = film.Year;
            EditFilm.UrlImage = film.UrlImage;
            EditFilm.Countrys = _testConunty(film.Countrys.NameOfTheCountry);

            #region Добавление Актёров
            //проверка если такие актёры у фильма в базе.
            //собираем все фильмы в которых снимался актёр
            IQueryable<FilmActor> filmActorsDBQ = _context.FilmActor.Where(x => x.FilmId == idf);
            List<FilmActor> filmActorsDB = filmActorsDBQ.ToList();
            if (filmActorsDB != null)
            {
                foreach (var item in film.FilmActor)
                {
                    if (!filmActorsDB.Any(x => x.ActorId == item.ActorId))
                    {
                        EditFilm.FilmActor.Add(item);
                    }
                }
            }
            _context.SaveChanges();
            #endregion

            #region Добавление Жанров

            //удаление дубликатов жанров, в данных полученных от пользователя
            IQueryable<FilmGenre> filmGenreDBQ = _context.FilmGenre.Where(x => x.FilmId == idf);
            List<FilmGenre> filmGenreDB = filmGenreDBQ.ToList();
            if (filmGenreDB != null)
            {
                foreach (var item in film.FilmGenre)
                {
                    if (!filmGenreDB.Any(x => x.GenreId == item.GenreId))
                    {
                        EditFilm.FilmGenre.Add(item);
                    }
                }
                _context.SaveChanges();
            }
            #endregion

            _context.SaveChanges();

            return film;
        }
       
        //Удаление по Id
        public void DeleteFilm(int? id)
        {
            _context.Film.Remove(_context.Film.First(x => x.Id == id));
            _context.SaveChanges();
        }

    }
}



