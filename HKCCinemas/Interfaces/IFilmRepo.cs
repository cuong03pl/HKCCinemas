using HKCCinemas.Models;

namespace HKCCinemas.Interfaces
{
    public interface IFilmRepo
    {
        List<Film> GetAllFilms();
        Film GetFilmById(int id);
        bool CreateFilm(Film film);
        bool UpdateFilm(Film film);
        bool DeleteFilm(int id);
        int CountFilm();
    }
}
