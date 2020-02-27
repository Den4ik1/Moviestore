using MoviesShop.Data;
using System.Collections.Generic;

namespace MoviesShop.Models
{
    public class Countrys : IData
    {
        public int Id { get; set; }
        public string NameOfTheCountry { get; set; }

        public ICollection<Film> Film { get; set; }
        public ICollection<Actor> Actor { get; set; }

        public Countrys()
        {
            Film = new List<Film>();
            Actor = new List<Actor>();
        }
    }
}
