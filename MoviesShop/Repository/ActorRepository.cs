using Microsoft.EntityFrameworkCore;
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

        //Вывод всех актёров
        public IQueryable<Actor> GetFullActor()
        {
            var t = _context.Actor
                .Include(af => af.FilmActor)
                .ThenInclude(f => f.Film)
                .Include(ac => ac.Country);
            var r = t.ToList();
            return t;
        }

        //Поиск по Id
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

        //Добавление актёра
        public void AddActor(ActorDTO _actor)
        {
            //Вывод всех актёров из бд для проверки
            var ActorsDBQ = _context.Actor;
            var ActorsDB = ActorsDBQ.ToList();

            var newActor = _actor.ConvertToActor();
            
            //Есть ли такой актёр уже в базе?
            if (!ActorsDB.Any(x =>
             (x.Name == newActor.Name) &&
             (x.BirthDay == newActor.BirthDay) &&
             (x.Country.NameOfTheCountry == newActor.Country.NameOfTheCountry)))
            {
                _context.Actor.Add(newActor);
            }
            _context.SaveChanges();
        }

        //Редактирование актёра
        //Изменение личных данных актёра (кроме фильмов в которых он снимался)
        //Добавление новых записей в связную таблицу
        public void EditActor(int? id, ActorDTO _actor)
        {
            int ida = (int)id;
            //Модель Actor с заполенной FilmActor
            Actor actor = new Actor();
            actor = _actor.ConvertToActor();

            //Вносим исправления в данные актёра.
            var ActorBD = _context.Actor.First(x => x.Id == ida);
            ActorBD.Name = actor.Name;
            ActorBD.Country = _testConunty(_actor.CountryDTO.TitleView);

            foreach (var item in actor.FilmActor)
            {
                    ActorBD.FilmActor.Add(item);
            }
            _context.SaveChanges();
        }

        //удаление по Id
        public void DeleteActor(int? Id)
        {
            _context.Actor.Remove(_context.Actor.First(x => x.Id == Id));
            _context.SaveChanges();
        }
    }
}
