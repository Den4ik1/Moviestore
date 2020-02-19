using System.Collections.Generic;

namespace MoviesShop.DTO
{
    //Полная информация о фильме
    public class FilmDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string UrlImage { get; set; }
        public List<TitleDTO> GenreDTO { get; set; }
        public List<ActorDTO> ActorDTO { get; set; } 
        public TitleDTO CountryDTO { get; set; } 

        public FilmDTO()
        {
            GenreDTO = new List<TitleDTO>();
            ActorDTO = new List<ActorDTO>();
        }
    }
}
