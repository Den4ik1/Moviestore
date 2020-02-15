using System.ComponentModel.DataAnnotations;

namespace MoviesShop.Models
{
    //Промежуточная таблица Пользователей и Фильмов
    public class UserFilm
    {
        [Key]
        public int Id { get; set; }

        public int UserId {get; set;}
        public User User {get; set;}
        public int FilmId {get; set;}
        public Film Film {get; set; }
    }
}
