using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesShop.DTO;
using MoviesShop.Models;
using MoviesShop.Repository;
using System.Collections.Generic;
using System.Linq;
using MoviesShop.Mappers;

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

        //Вывод (всех пользователей)/(или по Id)
        [HttpGet("{id?}")]
        public List<UserDTO> GetUsers(int? id)
        {
            List<UserDTO> user = new List<UserDTO>();

            if (id.HasValue)
            {
                user.Add(_repository.GetUserForId(id).ConvertToUserDTO());
                return user;
            }
            
            foreach (var item in _repository.GetUsers().ToList())
            {
                user.Add(item.ConvertToUserDTO());
            }
            return user;
        }

        //Поиск по имени
        [HttpGet("Name/{name}")]
        public List<UserDTO> getUserName(string name)
        {
            List<UserDTO> user = new List<UserDTO>();

            foreach (var item in _repository.GetUserForName(name).ToList())
            {
                user.Add(item.ConvertToUserDTO());
            };
            return user;
        }

        //Создание/Редактирование пользователя
        [HttpPost]
        public UserDTO PostUser(int? id, [FromBody] UserDTO _user)
        {
            if (id == 0)
            {
                _repository.AddUser(_mapper.Map<User>(_user));
                return _user;
            }
            _repository.EditUser(id, _mapper.Map<User>(_user));
            return _user;
        }

        //Удаление пользователя по Id
        [HttpDelete("{id?}")]
        public void Delete(int? id)
        {
            _repository.DeleteUser(id);
        }
    }
}