using Microsoft.AspNetCore.Mvc;
using MoviesShop.Repository;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MoviesShop.DTO;
using MoviesShop.Mappers;

namespace MoviesShop.Controllers
{
    [Route("api/[controller]")]
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
                return new List<ActorDTO>() { _mapper.Map<ActorDTO>(_repository.GetId(Id)) };
            }
            //var r = _mapper.Map<List<ActorDTO>>(_repository.GetFullActor().ToList());
            List<ActorDTO> actor = new List<ActorDTO>();
            foreach (var item in _repository.GetFullActor().ToList())
            {
                actor.Add(item.ConvertToActorDTO());
            }

            return actor;
        }
        
        //Поиск по имени
        [HttpGet("Name/{name}")]
        public List<ActorDTO> getActorName(string name)
        {
            return _mapper.Map<List<ActorDTO>>(_repository.GetActorName(name)).ToList();
        }
        
        // Создание/редактирование актёра
        [HttpPost("{Id?}")]
        public ActorDTO PostActor(int? Id, [FromBody] ActorDTO _actor)
        {
            //Удаление дублекатов во влеженном списке
            if (Id == 0)
            {
                _repository.AddActor(_actor);
                return _actor;
            }
            _repository.EditActor(Id, _actor);
            return _actor;
        }

        //Удаление актёра по Id
        [HttpDelete("{Id?}")]
        public void Delete(int? Id)
        {
            _repository.DeleteActor(Id);
        }
    }
}