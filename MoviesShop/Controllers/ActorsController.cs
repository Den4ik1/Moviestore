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
        public List<ResponseActorDTO> GetFullActors(int? id)
        {
            List<ResponseActorDTO> actor = new List<ResponseActorDTO>();

            if (id.HasValue)
            {
                actor.Add(_repository.GetForId(id).ConvertToResponseActor());
                return actor;
            }
            foreach (var item in _repository.GetFullActor().ToList())
            {
                actor.Add(item.ConvertToResponseActor());
            }

            return actor;
        }
        
        //Поиск по имени
        [HttpGet("Name/{name}")]
        public List<ResponseActorDTO> GetActorName(string name)
        {
            List<ResponseActorDTO> actor = new List<ResponseActorDTO>();

            foreach (var item in _repository.GetForName(name).ToList())
            {
                actor.Add(item.ConvertToResponseActor());
            };
            return actor;
        }
        
        // Создание/редактирование актёра
        [HttpPost("{id?}")]
        public ResponseActorDTO PostActor(int? id, [FromBody] RequestActorDTO actor)
        {
            if (id == 0)
            {
                return _repository.AddActor(actor.ConvertToRequestActor())
                    .ConvertToResponseActor();
            }
            return _repository.EditActor(id, actor.ConvertToRequestActor())
                .ConvertToResponseActor();
        }

        //Удаление актёра по Id
        [HttpDelete("{id?}")]
        public void Delete(int? id)
        {
            _repository.DeleteActor(id);
        }
    }
}