using AutoMapper;
using MoviesShop.DTO;
using MoviesShop.Models;
using System.Linq;

namespace MoviesShop.Mappers
{
    public class ToFilmDTO : Profile
    {
        public ToFilmDTO()
        {
            CreateMap<Countrys, TitleDTO>()
                .ForMember(dto => dto.Title, opt => opt.MapFrom(c => c.NameOfTheCountry))
                .ReverseMap();

            CreateMap<Genre, TitleDTO>()
                .ForMember(dto => dto.Title, opt => opt.MapFrom(g => g.Title))
                .ReverseMap();

            CreateMap<Actor, TitleDTO>()
                .ForMember(dto => dto.Title, opt => opt.MapFrom(a => a.Name))
                .ReverseMap();

            CreateMap<Film, FilmDTO>()
                 .ForMember(dto => dto.ActorDTO, opt => opt.MapFrom(fa => fa.FilmActor.Select(f => f.Actor).ToList()))
                 .ForMember(dto => dto.GenreDTO, opt => opt.MapFrom(fa => fa.FilmGenre.Select(f => f.Genre).ToList()))
                 .ForMember(dto => dto.CountryDTO, opt => opt.MapFrom(ca => ca.Countrys))
                 .ReverseMap();

        }
    }
}
