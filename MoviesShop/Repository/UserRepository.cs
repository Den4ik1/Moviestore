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
        public IQueryable<User> GetUserFilms()
        {
            var t = _context.User.Include(uf => uf.UserFilm).ThenInclude(f => f.Film);
            return t;
        }

        //Вывод по ID
        public User GetUserId(int? Id)
        {
            var result = _context.User.Include(uf => uf.UserFilm).ThenInclude(f => f.Film)
                 .First(x => x.Id == Id);
            if (result != null)
            {
                return result;
            }
            return new User();
        }

        //Добавление User
        public void AddUser( User _user)
        {
           _context.User.Add(_user);
        }

        //Редактирование пользователя
        public void EditUser(int? Id, User _user)
        {
            var temp = _context.User.FirstOrDefault(x => x.Id == Id);
            if (temp != null)
            {
                temp.Name = _user.Name;
                temp.Age = _user.Age;
                temp.Loggin = _user.Loggin;
                temp.Password = _user.Password;
                _context.SaveChanges();
            }
        }

        //Поиск по имени
        public IQueryable<User> GetUserName(string name)
        {
            var result = _context.User.Include(uf => uf.UserFilm)
               .ThenInclude(f => f.Film)
               .Where(p => p.Name.Contains($"{name}"));
            return result;
        }
        
        //Удаление по ID
        public void DeleteUser(int? Id)
        {
            _context.User.Remove(_context.User.First(x => x.Id == Id));
            _context.SaveChanges();
        }
    }
}
