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
        public TitleDTO CountryDTO { get; set; }

        public List<TitleDTO> GenreDTO { get; set; }
        public List<FilmItemDTO> FilmGenreDTO { get; set; }

        public List<TitleDTO> ActorDTO { get; set; }
        public List<FilmItemDTO> FilmActorDTO { get; set; }

        public FilmDTO()
        {

            GenreDTO = new List<TitleDTO>();
            ActorDTO = new List<TitleDTO>();
            FilmActorDTO = new List<FilmItemDTO>();
            FilmGenreDTO = new List<FilmItemDTO>();
        }
    }
}
