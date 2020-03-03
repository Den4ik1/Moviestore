using System;
using System.Collections.Generic;

namespace MoviesShop.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? BirthDay { get; set; }
        public Countrys Country { get; set; }
        
        public ICollection<FilmActor> FilmActor { get; set; }

        public Actor()
        {
            FilmActor = new List<FilmActor>();
        }
    }    
}
