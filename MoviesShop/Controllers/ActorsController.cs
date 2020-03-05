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
        [HttpGet("[action]")]
        public List<ResponseActorDTO> GetActors([FromBody] RequestActorDTO requestActor)
        {
            List<ResponseActorDTO> responseActor = new List<ResponseActorDTO>();
            
            //если пораметро для поиска не пришло,
            //создаётся пустая модель,
            //что бы не поломался Mapper
            if (requestActor == null)
            {
                requestActor = new RequestActorDTO();
            }

            //Вывод всех актёров по запросу
            var actor = _repository.GetActor(requestActor.ConvertToActor()).ToList();

            //Конветрация актёров к модели для вывода
            foreach (var item in actor)
            {
                responseActor.Add(item.ConvertToResponseActor());
            }

            return responseActor;
        }

        // Создание/редактирование актёра
        [HttpPost("[action]")]
        public ResponseActorDTO PostActor([FromBody] RequestActorDTO RequestActor)
        {
            if (RequestActor.Id > 0)
            {
                return _repository.EditActor(RequestActor.ConvertToActor())
                  .ConvertToResponseActor();
            }
            return _repository.AddActor(RequestActor.ConvertToActor())
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