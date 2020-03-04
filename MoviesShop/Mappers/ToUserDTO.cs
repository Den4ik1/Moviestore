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
            CreateMap<Film, ResponseFilmDTO>()
                .ForMember(dto => dto.Title, opt => opt.MapFrom(f => f.Title))
                .ForMember(dto => dto.GenreDTO, opt => opt.MapFrom(fg => fg.FilmGenre.Select(g => g.Genre).ToList()))
                .ReverseMap();

            CreateMap<Genre, GenreDTO>()
                .ForMember(dto => dto.Title, opt => opt.MapFrom(g => g.Title))
                .ReverseMap();

            CreateMap<User, UserDTO>()
                .ForMember(dto => dto.FilmsDTO, opt => opt.MapFrom(uf => 
                uf.UserFilm.Select(f => f.Film).ToList()))
                .ReverseMap();
              
        }
    }
}
