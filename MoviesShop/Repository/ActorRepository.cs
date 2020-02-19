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
            int ids = (int)id;
            //Модель Actor с частично заполенной моделью FilmActor
            Actor actor = new Actor();
            actor = _actor.ConvertToActor();

            //Вносим исправления в данные актёра.
            var ActorBD = _context.Actor.First(x => x.Id == id);
            ActorBD.Name = actor.Name;
            ActorBD.Country = _testConunty(_actor.CountryDTO.Title);

            //удаление дубликатов фильмов, в данных полученных от пользователя
            List<FilmActor> temp = new List<FilmActor>();
            foreach (var item in actor.FilmActor)
            {
                if (!temp.Any(x => x.FilmId == item.FilmId))
                {
                    temp.Add(item);
                }
            }

            actor.FilmActor = new List<FilmActor>();

            //проверка если такие фильмы у актёра в базе.
            IQueryable<FilmActor> DataDBQ = _context.FilmActor.Where(x => x.ActorId == id);
            List<FilmActor> DataDB = DataDBQ.ToList();

            foreach (var item in DataDB)
            {
                temp.Remove(temp.First(x => x.FilmId == item.FilmId));
            }

            foreach (var item in temp)
            {
                _context.FilmActor.Add(item);
            }

            _context.SaveChanges();

        }

        //Добавление актёра
        public void AddActor(ActorDTO _actor)
        {
            var newActor = new Actor()
            {
                Name = _actor.Name,
                BirthDay = _actor.BirthDay,
            };

            newActor.Country = _testConunty(_actor.CountryDTO.Title);

            _context.Actor.Add(newActor);
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
