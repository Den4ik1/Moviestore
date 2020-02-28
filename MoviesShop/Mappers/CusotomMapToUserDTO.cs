using MoviesShop.DTO;
using MoviesShop.Models;
using System.Collections.Generic;
using System.Linq;

namespace MoviesShop.Mappers
{
    public static class CusotomMapToUserDTO
    {
        public static UserDTO ConvertToUserDTO(this User @this)
        {
            //Выводим все жанры в List
            List<string> genreTitle = new List<string>();

            List<FilmDTO> films = new List<FilmDTO>();
            foreach (var item in @this.UserFilm)
            {
                List<GenreDTO> genres = new List<GenreDTO>();

                foreach (var itemgenre in item.Film.FilmGenre)
                {
                    genres.Add(new GenreDTO() { Title = itemgenre.Genre.Title });
                    genreTitle.Add(itemgenre.Genre.Title);
                }

                //Заполнение модели FilmDTO
                films.Add(new FilmDTO()
                {
                    Id = item.Film.Id,
                    Title = item.Film.Title,
                    GenreDTO = genres
                });
            }

            //Определение, жана и как часто встречается у пользователя.
            var genreArreyString = genreTitle.ToArray();
            GenreDTO[] genreDTOs = new GenreDTO[genreTitle.Distinct().ToList().Count];
            GenreDTO best = new GenreDTO();
            for (int i = 0; i < genreTitle.Distinct().ToList().Count; i++)
            {
                genreDTOs[i] = new GenreDTO();
            }
            int max = 0;
            foreach (var itemArreyString in genreArreyString)
            {
                foreach (var item in genreDTOs)
                {
                    if (itemArreyString == item.Title)
                    {
                        item.Count++;
                        if (max < item.Count)
                        {
                            max = item.Count;
                            best.Title = item.Title;
                        } 
                        break;
                    }
                    else
                    {
                        if (item.Title == null)
                        {
                            item.Title = itemArreyString;
                            item.Count++;
                            if (max < item.Count)
                            {
                                max = item.Count;
                                best.Title = item.Title;
                            }
                            break;
                        }
                    }
                }
            }
            //////////////////////////////////////////////////////////

            //Заполнение модели UserDTO
            UserDTO user = new UserDTO()
            {
                Id = @this.Id,
                Name = @this.Name,
                Age = @this.Age,
                FilmsDTO = films,
                BestGenre = best
            };

            return user;
        }
    }
}
