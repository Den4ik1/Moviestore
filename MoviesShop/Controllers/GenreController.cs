using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesShop.DTO;
using MoviesShop.Repository;
using System.Collections.Generic;
using System.Linq;

namespace MoviesShop.Controllers
{
    [Route("api/[controller]")]
    public class GenreController
    {
        private readonly GenreRepository _repository;
        private readonly IMapper _mapper;
        public GenreController(GenreRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public List<GenreDTO> GetListGenre()
        {
            return _mapper.Map<List<GenreDTO>>(_repository.GetGenres().ToList());
        }
    }
}