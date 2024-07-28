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
            CreateMap<Category, CategoryViewDTO>();
            CreateMap<CategoryViewDTO, Category>();
            CreateMap<CinemasDTO, Cinemas>();
            CreateMap<Cinemas, CinemasDTO>();
            CreateMap<Trailer, TrailerDTO>();
            CreateMap<TrailerDTO, Trailer>();
            CreateMap<Comment, CommentDTO>();
            CreateMap<CommentDTO, Comment>();
            CreateMap<CinemasCategory, CinemasCategoryDTO>();
            CreateMap<CinemasCategoryDTO, CinemasCategory>();
            CreateMap<SeatDTO, Seat>();
            CreateMap<Seat, SeatDTO>();
            CreateMap<Ticket, TicketDTO>();
            CreateMap<TicketDTO, Ticket>();
            CreateMap<RoomDTO, Room>();
            CreateMap<Room, RoomDTO>();
            CreateMap<ShowDate, ShowDateDTO>();
            CreateMap<ShowDateDTO, ShowDate>();
            CreateMap<Schedule, ScheduleDTO>();
            CreateMap<ScheduleDTO, Schedule>();
            CreateMap<BookingDetail, BookingDetailDTO>();
            CreateMap<BookingDetailDTO, BookingDetail>();
            CreateMap<Favourite, FavouriteDTO>();
            CreateMap<FavouriteDTO, Favourite>();
        }
    }
}
