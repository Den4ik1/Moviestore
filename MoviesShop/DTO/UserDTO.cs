using System.Collections.Generic;

namespace MoviesShop.DTO
{
    //Модель пользователя (без логина и пороля)
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        //public GenreDTO[] GenresDTO { get; set; }
        public GenreDTO BestGenre { get; set; }
        public ICollection<ResponseFilmDTO> FilmsDTO { get; set; }

        public UserDTO()
        {
            FilmsDTO = new List<ResponseFilmDTO>();
        }
    }
}
