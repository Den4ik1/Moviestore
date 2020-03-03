using Microsoft.AspNetCore.Mvc;
using MoviesShop.Repository;
using System.Collections.Generic;
using System.Linq;
using MoviesShop.DTO;
using MoviesShop.Mappers;

namespace MoviesShop.Controllers
{
    [Route("api/[controller]")]
    public class ActorsController
    {
        private readonly ActorRepository _repository;

        public ActorsController(ActorRepository repository)
        {
            _repository = repository;
        }

        //Вывод полной информации о (всех актёрах) / (по Id)
        [HttpGet("{id?}")]
        public List<ActorDTO> GetFullActors(int? id)
        {
            List<ActorDTO> actor = new List<ActorDTO>();

            if (id.HasValue)
            {
                actor.Add(_repository.GetForId(id).ConvertToActorDTO());
                return actor;
            }
            foreach (var item in _repository.GetFullActor().ToList())
            {
                actor.Add(item.ConvertToActorDTO());
            }

            return actor;
        }
        
        //Поиск по имени
        [HttpGet("Name/{name}")]
        public List<ActorDTO> GetActorName(string name)
        {
            List<ActorDTO> actor = new List<ActorDTO>();

            foreach (var item in _repository.GetForName(name).ToList())
            {
                actor.Add(item.ConvertToActorDTO());
            };
            return actor;
        }
        
        // Создание/редактирование актёра
        [HttpPost("{id?}")]
        public ActorDTO PostActor(int? id, [FromBody] ActorDTO _actor)
        {
            if (id == 0)
            {
                _repository.AddActor(_actor);
                return _actor;
            }
            _repository.EditActor(id, _actor);
            return _actor;
        }

        //Удаление актёра по Id
        [HttpDelete("{id?}")]
        public void Delete(int? id)
        {
            _repository.DeleteActor(id);
        }
    }
}