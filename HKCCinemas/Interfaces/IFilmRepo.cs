﻿using HKCCinemas.DTO;
using HKCCinemas.Helper;
using HKCCinemas.Models;

namespace HKCCinemas.Interfaces
{
    public interface IFilmRepo
    {
        List<FilmViewDTO> GetAllFilms();
        List<Film> GetTop5Films();
        List<Film> GetAllFilmByCategory(int cateId);
        List<int> GetAllFilmByActor(int actorId);
        Film GetFilmById(int id);
        List<Film> GetNowShowingFilms();
        List<Film> GetUpcomingFilms();
        List<Film> GetSimilarFilm(int categoryId);
        Task<bool> CreateFilmAsync(FilmDTO film);
        Task<bool> UpdateFilmAsync(int id, FilmDTO film);
        bool DeleteFilm(int id);
        int Count();

        //search 
        List<FilmViewDTO> GetAllFilmsByQuery(QueryObject query);



    }
}
