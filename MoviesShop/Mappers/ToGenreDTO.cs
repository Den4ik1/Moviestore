using AutoMapper;
using MoviesShop.DTO;
using MoviesShop.Models;

namespace MoviesShop.Mappers
{
    public class ToGenreDTO : Profile
    {
        public ToGenreDTO()
        {
            CreateMap<Genre, GenreDTO>()
                .ReverseMap();
        }
    }
}
