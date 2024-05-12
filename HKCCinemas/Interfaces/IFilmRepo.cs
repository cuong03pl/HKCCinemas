using HKCCinemas.DTO;
using HKCCinemas.Models;

namespace HKCCinemas.Interfaces
{
    public interface IFilmRepo
    {
        List<Film> GetAllFilms();
        List<Film> GetTop5Films();
        Film GetFilmById(int id);
        List<Film> GetNowShowingFilms();
        List<Film> GetUpcomingFilms();
        List<Film> GetSimilarFilm(int categoryId);
        Task<bool> CreateFilmAsync(FilmDTO film);
        Task<bool> UpdateFilmAsync(int id, FilmDTO film);
        bool DeleteFilm(int id);
        int CountFilm();
    }
}
