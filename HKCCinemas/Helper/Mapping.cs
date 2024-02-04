using AutoMapper;
using HKCCinemas.DTO;
using HKCCinemas.Models;

namespace HKCCinemas.Helper
{
    public class Mapping : Profile
    {
        public Mapping() {
            CreateMap<ActorDTO, Actor>();
            CreateMap<Actor, ActorDTO>();
            CreateMap<FilmDTO, Film>();
            CreateMap<Film, FilmDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();
            CreateMap<CinemasDTO, Cinemas>();
            CreateMap<Cinemas, CinemasDTO>();
        }
    }
}
