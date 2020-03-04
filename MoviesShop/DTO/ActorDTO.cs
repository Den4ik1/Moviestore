using System;
using System.Collections.Generic;

namespace MoviesShop.DTO
{
    //Полная информация о Актёре
    public class ActorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime? BirthDay { get; set; }
        public TitleDTO CountryDTO { get; set; }
        public ICollection<RelationshipStagingDTO> FilmsActorDTO { get; set; }
        public ICollection<TitleDTO> FilmsDTO { get; set; }

        public ActorDTO()
        {
            FilmsActorDTO = new List<RelationshipStagingDTO>();
            FilmsDTO = new List<TitleDTO>();
        }
    }
}
