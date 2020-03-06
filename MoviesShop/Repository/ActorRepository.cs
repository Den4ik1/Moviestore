using Microsoft.EntityFrameworkCore;
using MoviesShop.Models;
using System.Linq;

namespace MoviesShop.Repository
{
    public class ActorRepository
    {
        private readonly MoviesShopContext _context;
        private delegate Countrys _сheckConunty(string titleCounty);
        private readonly _сheckConunty _testConunty;

        public ActorRepository(MoviesShopContext context)
        {
            _context = context;
            _testConunty = new ExistCountry(context).TestCountry;
        }

        public IQueryable<Actor> GetActor(Actor actor)
        {
            if (actor.Id > 0)
            {
                return _context.Actor
                    .Include(af => af.FilmActor)
                    .ThenInclude(f => f.Film)
                    .Include(ac => ac.Country)
                    .Where(f => f.Id == actor.Id);
            }
            else if (actor.Name != null && actor.Name != "")
            {
                return _context.Actor
                    .Where(p => p.Name.Contains($"{actor.Name}"))
                    .Include(af => af.FilmActor)
                    .ThenInclude(f => f.Film)
                    .Include(ac => ac.Country);
            }
            else
            {
                return _context.Actor
                    .Include(af => af.FilmActor)
                    .ThenInclude(f => f.Film)
                    .Include(ac => ac.Country);
            }
        }

        //Вывод всех актёров
        //public List<Actor> GetFullActor()
        //public IQueryable<Actor> GetFullActor()
        //{
        //    //var result = _context.Actor.ToList();//.Select(x=>new {filmAcrt = x.FilmActor, })

        //    //Рабочая штука
        //    var result = _context.Actor
        //    .Include(af => af.FilmActor)
        //    .ThenInclude(f => f.Film)
        //    .Include(ac => ac.Country);
        //    return result;
        //}

        //Поиск по Id
        //public Actor GetForId(int? id)
        //{
        //    var result = _context.Actor
        //        .Include(af => af.FilmActor)
        //        .ThenInclude(f => f.Film)
        //        .Include(ac => ac.Country)
        //        .First(f => f.Id == id);
        //    if (result != null)
        //    {
        //        return result;
        //    }
        //    return new Actor();
        //}

        //Поиск по имени
        //public IQueryable<Actor> GetForName(string name)
        //{
        //    var result = _context.Actor
        //        .Where(p => p.Name.Contains($"{name}"))
        //        .Include(af => af.FilmActor)
        //        .ThenInclude(f => f.Film)
        //        .Include(ac => ac.Country);
        //    return result;
        //}

        //Добавление актёра
        
        public Actor AddActor(Actor actor)
        {
            //Вывод всех актёров из бд для проверки
            var ActorsDBQ = _context.Actor.Include(x => x.Country);
            var ActorsDB = ActorsDBQ.ToList();

            //var newActor = _actor.ConvertToActor();

            //Проверка, eсть ли такой актёр уже в базе?
            if (!ActorsDB.Any(x =>
             (x.Name == actor.Name) &&
             (x.BirthDay == actor.BirthDay) &&
             (x.Country.NameOfTheCountry == actor.Country.NameOfTheCountry)))
            {
                actor.Country = _testConunty(actor.Country.NameOfTheCountry);
                _context.Actor.Add(actor);
                _context.SaveChanges();
                return GetFirstActorForId(actor.Id);
            }
            return new Actor();
        }

        //Редактирование актёра
        //Изменение личных данных актёра (кроме фильмов в которых он снимался)
        //Добавление новых записей в связную таблицу
        public Actor EditActor(Actor actor)
        {
            //Вносим исправления для актёра.
            var ActorBD = _context.Actor.First(x => x.Id == actor.Id);
            ActorBD.Name = actor.Name;
            ActorBD.BirthDay = actor.BirthDay;
            ActorBD.Country = _testConunty(actor.Country.NameOfTheCountry);
            foreach (var item in actor.FilmActor)
            {
                ActorBD.FilmActor.Add(item);
                var r = actor.FilmActor.ToList();
            }
            _context.SaveChanges();
            var s = GetFirstActorForId(actor.Id);
            return s;
        }

        public Actor GetFirstActorForId(int id)
        {
            return _context.Actor
                .Include(af => af.FilmActor)
                .ThenInclude(f => f.Film)
                .Include(ac => ac.Country)
                .First(f => f.Id == id);
        }

        //удаление по Id
        public void DeleteActor(int? id)
        {
            _context.Actor.Remove(_context.Actor.First(x => x.Id == id));
            _context.SaveChanges();
        }
    }
}
