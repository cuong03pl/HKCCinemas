using HKCCinemas.DTO;
using HKCCinemas.Models;

namespace HKCCinemas.Interfaces
{
    public interface IFilmRepo
    {
        List<Film> GetAllFilms();
        Film GetFilmById(int id);
        Task<bool> CreateFilmAsync(FilmDTO film);
        Task<bool> UpdateFilmAsync(int id, FilmDTO film);
        bool DeleteFilm(int id);
        int CountFilm();
    }
}
