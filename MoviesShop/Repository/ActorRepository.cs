﻿using Microsoft.EntityFrameworkCore;
using MoviesShop.DTO;
using MoviesShop.Models;
using System.Collections.Generic;
using System.Linq;
using MoviesShop.Mappers;

namespace MoviesShop.Repository
{
    public class ActorRepository
    {
        private readonly MoviesShopContext _context;
        private delegate Countrys _сheckConunty(string titleCounty);
        private _сheckConunty _testConunty;

        public ActorRepository(MoviesShopContext context)
        {
            _context = context;
            _testConunty = new ExistCountry(context).TestCountry;
        }

        public IQueryable<Actor> GetFullActor()
        {
            var t = _context.Actor
                .Include(af => af.FilmActor)
                .ThenInclude(f => f.Film)
                .Include(ac => ac.Country);
            return t;
        }

        public Actor GetId(int? Id)
        {
            var result = _context.Actor
                .Include(af => af.FilmActor)
                .ThenInclude(f => f.Film)
                .Include(ac => ac.Country)
                .First(f => f.Id == Id);
            if (result != null)
            {
                return result;
            }
            return new Actor();
        }

        //Редактирование актёра
        //Изменение личных данных актёра (кроме фильмов в которых он снимался)
        //Проверка на дублекаты в фильмах, по промежуточной таблице.
        //Добавление новых записей в связную таблицу
        public void EditActor(int? id, ActorDTO _actor)
        {
            int ida = (int)id;
            //Модель Actor с частично заполенной моделью FilmActor
            Actor actor = new Actor();
            actor = _actor.ConvertToActor();

            //Вносим исправления в данные актёра.
            var ActorBD = _context.Actor.First(x => x.Id == ida);
            ActorBD.Name = actor.Name;
            ActorBD.Country = _testConunty(_actor.CountryDTO.Title);

            //удаление дубликатов фильмов, в данных полученных от пользователя
            List<FilmActor> filmActor = new List<FilmActor>();
            foreach (var item in actor.FilmActor)
            {
                if (!filmActor.Any(x => x.FilmId == item.FilmId))
                {
                    item.ActorId = ida;
                    filmActor.Add(item);
                }
            }

            actor.FilmActor = new List<FilmActor>();
            //проверка если такие фильмы у актёра в базе.
            //собираем все фильмы в которых снимался актёр
            IQueryable<FilmActor> actorFilmsDBQ = _context.FilmActor.Where(x => x.ActorId == ida);
            List<FilmActor> actorFilmsDB = actorFilmsDBQ.ToList();

            foreach (var item in actorFilmsDB)
            {
                filmActor.Remove(filmActor.First(x => x.FilmId == item.FilmId));
            }

            foreach (var item in filmActor)
            {
                _context.FilmActor.Add(item);
            }

            _context.SaveChanges();

        }

        //Добавление актёра
        public void AddActor(ActorDTO _actor)
        {
            var ActorsDBQ = _context.Actor;
            var ActorsDB = ActorsDBQ.ToList();

            var newActor = _actor.ConvertToActor();

            //Проверка, есть ли такой актёр в БД
            if (!ActorsDB.Any(x =>
             (x.Name == newActor.Name) &&
             (x.BirthDay == newActor.BirthDay) &&
             (x.Country.NameOfTheCountry == newActor.Country.NameOfTheCountry)))
            {
                _context.Actor.Add(newActor);
            }
            _context.SaveChanges();

            //удаление дубликатов фильмов, в данных полученных от пользователя
            List<FilmActor> filmActor = new List<FilmActor>();
          
            foreach (var item in newActor.FilmActor)
            {
                if (!filmActor.Any(x => x.FilmId == item.FilmId))
                {
                    FilmActor temp = new FilmActor() { FilmId = item.FilmId, ActorId = item.ActorId };
                    filmActor.Add(temp);
                }
            }
            _context.SaveChanges();

        }

        //Поиск по имени
        public IQueryable<Actor> GetActorName(string name)
        {
            var result = _context.Actor
                .Include(af => af.FilmActor)
                .ThenInclude(f => f.Film)
                .Include(ac => ac.Country)
                .Where(p => p.Name.Contains($"{name}"));
            return result;
        }

        //удаление по Id
        public void DeleteActor(int? Id)
        {
            _context.Actor.Remove(_context.Actor.First(x => x.Id == Id));
            _context.SaveChanges();
        }
    }
}
