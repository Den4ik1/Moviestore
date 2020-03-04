using System;
using System.Collections.Generic;

namespace MoviesShop.DTO
{
    public class ResponseActorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? BirthDay { get; set; }
        public CountryDTO CountryDTO { get; set; }

        public ICollection<ResponseFilmDTO> FilmsDTO { get; set; }

        public ResponseActorDTO()
        {
            FilmsDTO = new List<ResponseFilmDTO>();
        }
    }
}
