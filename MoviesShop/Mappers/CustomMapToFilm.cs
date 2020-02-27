﻿using MoviesShop.DTO;
using MoviesShop.Models;
using System.Collections.Generic;

namespace MoviesShop.Mappers
{
    public static class CustomMapToFilm
    {
        public static Film ConvertToFilme(this FilmDTO @this)
        {
            //заполениен FilmActor
            List<FilmActor> filmActors = new List<FilmActor>();
            foreach (var item in @this.FilmActorDTO)
            {
                filmActors.Add(new FilmActor() { ActorId = item.SecondId});
            }

            //заполениен FilmGenre
            List<FilmGenre> filmGenres = new List<FilmGenre>();
            foreach (var item in @this.FilmGenreDTO)
            {
                filmGenres.Add(new FilmGenre() { GenreId = item.SecondId});
            }

            //заполениен Film
            Film film = new Film()
            {
                Id = @this.Id,
                Title = @this.Title,
                Year = @this.Year,
                Countrys = new Countrys() { NameOfTheCountry = @this.CountryDTO.TitleView },
                FilmActor = filmActors,
                FilmGenre = filmGenres
            };
            return film;
        }
    }
}
