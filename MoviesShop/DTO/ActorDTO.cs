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
        public ICollection<TitleDTO> Films { get; set; }

        public ActorDTO()
        {
            Films = new List<TitleDTO>();
        }
    }
}
