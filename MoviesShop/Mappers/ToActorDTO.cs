using AutoMapper;
using MoviesShop.DTO;
using MoviesShop.Models;
using System.Linq;

namespace MoviesShop.Mappers
{
    public class ToActorDTO : Profile
    {
        public ToActorDTO()
        {
            CreateMap<Countrys, TitleDTO>()
                .ForMember(dto => dto.Title, opt => opt.MapFrom(c => c.NameOfTheCountry))
                .ReverseMap();

            CreateMap<Film, TitleDTO>()
                .ForMember(dto => dto.Title, opt => opt.MapFrom(c => c.Title))
                .ReverseMap();

            CreateMap<FilmItemDTO, FilmActor>()
                .ReverseMap();

            CreateMap<Actor, ActorDTO>()
                .ForMember(dto => dto.FilmsDTO, opt => opt.MapFrom(fa => fa.FilmActor.Select(f => f.Film).ToList()))
                .ForMember(dto => dto.CountryDTO, opt => opt.MapFrom(ca => ca.Country))

                .ReverseMap();
        }
    }
}
