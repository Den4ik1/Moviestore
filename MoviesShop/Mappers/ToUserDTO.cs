using MoviesShop.Models;
using AutoMapper;
using MoviesShop.DTO;
using System.Linq;

namespace MoviesShop.Mappers
{
    public class ToUserDTO : Profile
    {
        public ToUserDTO()
        {
            CreateMap<Film, TitleDTO>()
                .ForMember(dto => dto.Title, opt => opt.MapFrom(f => f.Title))
                .ReverseMap();

            CreateMap<User, UserDTO>()
                .ForMember(dto => dto.Films, opt => opt.MapFrom(uf => 
                uf.UserFilm.Select(f => f.Film).ToList()))
                .ReverseMap();
              
        }
    }
}
