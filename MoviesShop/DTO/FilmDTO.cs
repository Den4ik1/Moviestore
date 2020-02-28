using System.Collections.Generic;

namespace MoviesShop.DTO
{
    public class FilmDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string UrlImage { get; set; }
        public CountryDTO CountryDTO { get; set; }

        public List<GenreDTO> GenreDTO { get; set; }
        public List<RelationshipStagingDTO> FilmGenreDTO { get; set; }

        public List<ActorDTO> ActorDTO { get; set; }
        public List<RelationshipStagingDTO> FilmActorDTO { get; set; }

        public FilmDTO()
        {
            GenreDTO = new List<GenreDTO>();
            ActorDTO = new List<ActorDTO>();
            FilmActorDTO = new List<RelationshipStagingDTO>();
            FilmGenreDTO = new List<RelationshipStagingDTO>();
        }
    }
}
