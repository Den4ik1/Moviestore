using Microsoft.AspNetCore.Mvc;
using MoviesShop.Models;
using MoviesShop.Repository;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MoviesShop.DTO;

namespace MoviesShop.Controllers
{
    [Route ("api/[controller]")]
    public class ActorsController 
    {
        private readonly ActorRepository _repository;
        private readonly IMapper _mapper;

        public ActorsController(ActorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        //Вывод полной информации о фсех актёрах
        [HttpGet("{Id?}")]
        public List<ActorDTO> GetFullActors(int? Id)
        {

            if (Id.HasValue)
            {
                return _mapper.Map<List<ActorDTO>>(new List<Actor>() { _repository.GetId(Id) });
            }
            return _mapper.Map<List<ActorDTO>>(_repository.GetFullActor().ToList());
        }

        // Создание/редактирование актёра
        [HttpPost("{Id?}")]
        public ActorDTO PostActor(int? Id, [FromBody] ActorDTO _actor)
        {
            if (Id == 0)
            {
                _repository.AddActor(_actor);
                return _actor;
            }
            _repository.EditActor(Id,  _actor);
            return _actor;
           // return new ActorDTO();
        }

        //Поиск по названию
        [HttpGet("Name/{name}")]
        public List<ActorDTO> getActorName(string name)
        {
            var temp = _repository.GetActorName(name);
            return _mapper.Map<List<ActorDTO>>(temp).ToList();
        }

        //Удаление актёра по Id
        [HttpDelete("{Id?}")]
        public void Delete(int? Id)
        {
            _repository.DeleteActor(Id);
        }
    }
}