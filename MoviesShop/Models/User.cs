using MoviesShop.Data;
using System.Collections.Generic;


namespace MoviesShop.Models
{
    public class User : IData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Loggin { get; set; }
        public string Password { get; set; }

        public ICollection<UserFilm> UserFilm { get; set; }

        public User()
        {
            UserFilm = new List<UserFilm>();
        }
    }
}
