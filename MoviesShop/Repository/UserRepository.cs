using Microsoft.EntityFrameworkCore;
using MoviesShop.Data;
using MoviesShop.Models;
using System.Linq;

namespace MoviesShop.Repository
{
    public class UserRepository 
    {
        private readonly MoviesShopContext _context; 

        public UserRepository(MoviesShopContext context) 
        {
            _context = context;
            new ContextInitializer().Seed(_context);
        }

        //Вывод всех пользователей и фильмы которые они смотрели.
        public IQueryable<User> GetUsers()
        {
            var t = _context.User
                .Include(uf => uf.UserFilm)
                .ThenInclude(f => f.Film)
                .ThenInclude(gf => gf.FilmGenre).ThenInclude(x => x.Genre);
            var r = t.ToList();
            return t;
        }

        //Вывод по ID
        public User GetUserForId(int? id)
        {
            var result = _context.User.Include(uf => uf.UserFilm).ThenInclude(f => f.Film)
                 .First(x => x.Id == id);
            if (result != null)
            {
                return result;
            }
            return new User();
        }

        //Поиск по имени
        public IQueryable<User> GetUserForName(string name)
        {
            var result = _context.User.Include(uf => uf.UserFilm)
               .ThenInclude(f => f.Film)
               .Where(p => p.Name.Contains($"{name}"));
            return result;
        }

        //Добавление User
        public void AddUser( User _user)
        {
           _context.User.Add(_user);
           _context.SaveChanges();
        }

        //Редактирование пользователя
        public void EditUser(int? id, User _user)
        {
            var temp = _context.User.FirstOrDefault(x => x.Id == id);
            if (temp != null)
            {
                temp.Name = _user.Name;
                temp.Age = _user.Age;
                temp.Loggin = _user.Loggin;
                temp.Password = _user.Password;
                _context.SaveChanges();
            }
        }

        //Удаление по ID
        public void DeleteUser(int? id)
        {
            _context.User.Remove(_context.User.First(x => x.Id == id));
            _context.SaveChanges();
        }
    }
}
