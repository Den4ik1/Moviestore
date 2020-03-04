using System;
using System.Collections.Generic;

namespace MoviesShop.DTO
{
    public class RequestActorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? BirthDay { get; set; }
        public CountryDTO CountryDTO { get; set; }

        //public ICollection<RelationshipStagingDTO> FilmsActorDTO { get; set; }

        public RequestActorDTO()
        {
            //FilmsActorDTO = new List<RelationshipStagingDTO>();
        }
    }
}
