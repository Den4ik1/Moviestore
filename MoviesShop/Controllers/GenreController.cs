using Microsoft.AspNetCore.Mvc;
using MoviesShop.Models;
using MoviesShop.Repository;
using System.Collections.Generic;
using System.Linq;

namespace MoviesShop.Controllers
{
    [Route("api/[controller]")]
    public class GenreController
    {
        private readonly GenreRepository _repository;

        public GenreController(GenreRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public List<Genre> GetListGenre()
        {
            return _repository.GetGenres().ToList();
        }
    }
}