using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesShop.DTO;
using MoviesShop.Models;
using MoviesShop.Repository;
using System.Collections.Generic;
using System.Linq;

namespace MoviesShop.Controllers
{
    [Route("api/[controller]")]
    public class UsersController
    {
        private readonly IMapper _mapper;
        private readonly UserRepository _repository;

        public UsersController(UserRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        //Вывод всех пользователей
        [HttpGet("{Id?}")]
        public List<UserDTO> GetUsers(int? Id)
        {
            if (Id.HasValue)
            {
                return _mapper.Map<List<UserDTO>>(new List<User>() { _repository.GetUserId(Id)});
            }
            return _mapper.Map<List<UserDTO>>(_repository.GetUserFilms().ToList());
        }

        //Создание/Редактирование пользователя
        [HttpPost]
        public UserDTO PostUser(int? Id, [FromBody] UserDTO _user)
        {
            if (Id == 0)
            {
                _repository.AddUser(_mapper.Map<User>(_user));
                return _user;
            }
            _repository.EditUser(Id, _mapper.Map<User>(_user));
            return _user;
        }

        //Поиск по имени
        [HttpGet("Name/{name}")]
        public List<UserDTO> getUserName(string name)
        {
            var temp = _repository.GetUserName(name);
            return _mapper.Map<List<UserDTO>>(temp);
        }

        //Удаление пользователя по Id
        [HttpDelete("{Id?}")]
        public void Delete(int? Id)
        {
            _repository.DeleteUser(Id);
        }
    }
}