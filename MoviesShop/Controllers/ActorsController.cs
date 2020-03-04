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

        //Вывод полной информации о (всех актёрах) / (по Id) / (по имени)
        [HttpGet("All")]
        public List<ResponseActorDTO> GetAllActors([FromBody] RequestActorDTO RequestActor)
        {
            List<ResponseActorDTO> ResponseActor = new List<ResponseActorDTO>();
            if (RequestActor != null)
            {
                if (RequestActor.Id > 0)
                {
                    ResponseActor.Add(_repository.GetForId(RequestActor.Id).ConvertToResponseActor());
                    return ResponseActor;
                }
                else if (RequestActor.Name != null || RequestActor.Name != "")
                {
                    foreach (var item in _repository.GetForName(RequestActor.Name).ToList())
                    {
                        ResponseActor.Add(item.ConvertToResponseActor());
                    };
                }
            }
            else
            {
                foreach (var item in _repository.GetFullActor().ToList())
                {
                    ResponseActor.Add(item.ConvertToResponseActor());
                }
            }
            return ResponseActor;
        }

        [HttpPost("{id?}")]
        public ResponseActorDTO PostActor([FromBody] RequestActorDTO RequestActor)
        {
            if (RequestActor.Id > 0)
            {
                return _repository.EditActor(RequestActor.Id, RequestActor.ConvertToRequestActor())
                  .ConvertToResponseActor();
            }
            return _repository.AddActor(RequestActor.ConvertToRequestActor())
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