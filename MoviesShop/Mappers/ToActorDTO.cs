using AutoMapper;
using MoviesShop.DTO;
using MoviesShop.Models;
using System.Linq;

namespace MoviesShop.Mappers
{
    public class ToActorDTO : Profile
    {
        //public ToActorDTO()
        //{
        //    CreateMap<Countrys, CountryDTO>()
        //        .ForMember(dto => dto.CountryTitle, opt => opt.MapFrom(c => c.NameOfTheCountry))
        //        .ReverseMap();

        //    CreateMap<Film, FilmDTO>()
        //        .ForMember(dto => dto.Title, opt => opt.MapFrom(c => c.Title))
        //        .ReverseMap();

        //    CreateMap<Actor, ActorDTO>()
        //        .ForMember(dto => dto.FilmsDTO, opt => opt.MapFrom(fa => fa.FilmActor.Select(f => f.Film).ToList()))
        //        .ForMember(dto => dto.CountryDTO, opt => opt.MapFrom(ca => ca.Country))
        //        .ReverseMap();
        //}
    }
}
