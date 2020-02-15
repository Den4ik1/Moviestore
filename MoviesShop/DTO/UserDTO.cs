using System.Collections.Generic;

namespace MoviesShop.DTO
{
    //Допустимая модель пользователя (без логина и пороля)
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        //public string Loggin { get; set; }

        public ICollection<TitleDTO> Films { get; set; }

        public UserDTO()
        {
            Films = new List<TitleDTO>();
        }
    }
}
