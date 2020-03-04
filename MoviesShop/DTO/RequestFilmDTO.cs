using System;
using System.Collections.Generic;

namespace MoviesShop.DTO
{
    public class RequestFilmDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string UrlImage { get; set; }
        public CountryDTO CountryDTO { get; set; }

        public List<RelationshipStagingDTO> FilmGenreDTO { get; set; }

        public List<RelationshipStagingDTO> FilmActorDTO { get; set; }

        public RequestFilmDTO()
        {
            FilmActorDTO = new List<RelationshipStagingDTO>();
            FilmGenreDTO = new List<RelationshipStagingDTO>();
        }
    }
}
