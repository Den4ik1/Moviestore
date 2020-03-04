using System.Collections.Generic;

namespace MoviesShop.DTO
{
    public class ResponseFilmDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string UrlImage { get; set; }
        public CountryDTO CountryDTO { get; set; }

        public List<GenreDTO> GenreDTO { get; set; }

        public List<ResponseActorDTO> ActorDTO { get; set; }

        public ResponseFilmDTO()
        {
            GenreDTO = new List<GenreDTO>();
            ActorDTO = new List<ResponseActorDTO>();
        }
    }
}
