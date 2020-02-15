using Microsoft.EntityFrameworkCore;
using MoviesShop.DTO;
using MoviesShop.Models;
using System.Collections.Generic;
using System.Linq;

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

        /////////////////////////////////////////////////////////////////////////////
        /*тут всё ок) Работает. Проблема с переводом DTOModel к BaseModel */
        #region
        /////////////////////////////////////////////////////////////////////////////
        //public void AddActor(int? id, Actor _actor)                              //
        //{                                                                        //
        //    var temp = _context.Actor.FirstOrDefault(x => x.Id == id);           //
        //    if (temp != null)                                                    //
        //    {                                                                    //
        //        temp.Name = _actor.Name;                                         //
        //        temp.BirthDay = _actor.BirthDay;                                 //
        //        temp.Country = _actor.Country;                                   //
        //    }                                                                    //
        //    else                                                                 //
        //    {                                                                    //
        //        _context.Actor.Add(_actor);                                      //
        //    }                                                                    //
        //    _context.SaveChanges();                                              //
        // }                                                                       //
        //                                                                         //
        /////////////////////////////////////////////////////////////////////////////
        #endregion
                       
        //Редактирование актёра
        //Вывод его Id
        //Поиск Id фильма (если не, то создать новый)
        //Добавление в промежуточную  таблицу обоих Id
        public void EditActor(int? id, ActorDTO _actor)
        {
            ICollection<TitleDTO> filmList;
            try
            {
               filmList = _actor.Films;
            }
            catch
            {
                filmList = null;
            }
            if (!_context.Actor.Any(x => x.Name == _actor.Name))
            {
                var newActor = new Actor()
                {
                    Name = _actor.Name,
                    BirthDay = _actor.BirthDay,
                };

                newActor.Country = _testConunty(_actor.CountryDTO.Title);

                _context.Actor.Add(newActor);
            }
            else
            {
                var EditActor = _context.Actor.First(x => x.Id == id);
                EditActor.Name = _actor.Name;
                EditActor.BirthDay = _actor.BirthDay;
                EditActor.Country = _testConunty(_actor.CountryDTO.Title);
            }
            _context.SaveChanges();

            //добавление фильмов в которых актёр играл
            int IdActor =  _context.Actor.FirstOrDefault(x => x.Name == _actor.Name).Id;
            foreach (var item in filmList)
            {
                FilmActor fa = new FilmActor() { ActorId = IdActor };
                if (!_context.Film.Any(x => x.Title == item.Title))
                {
                    _context.Film.Add(new Film() { Title = item.Title, Countrys = new Countrys()});
                    _context.SaveChanges();
                    fa.FilmId = _context.Film.First(x => x.Title == item.Title).Id;
                }
                fa.FilmId = _context.Film.First(x => x.Title == item.Title).Id;
                _context.FilmActor.Add(fa);
                _context.SaveChanges();
            }

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
