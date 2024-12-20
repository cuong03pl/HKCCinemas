using HKCCinemas.DTO;
using HKCCinemas.Helper;
using HKCCinemas.Models;

namespace HKCCinemas.Interfaces
{
    public interface IScheduleRepo
    {
        List<ScheduleViewDTO> GetAllSchedule ();
        List<ScheduleViewDTO> GetAllScheduleByFilmIdAndRoomId(int filmId, int roomId);
        List<ScheduleViewDTO> GetAllScheduleByShowDateAndCinemas(DateTime date, int cinemasId);
        List<ScheduleViewDTO> GetScheduleByFilm(int filmId);
        List<ScheduleViewDTO> GetScheduleByShowDateAndFilm(DateTime date, int filmId);
        List<ShowDate> GetShowDatesByFilmId(int filmId);
        ScheduleDTO GetScheduleByShowDateAndCinemasAndFilm(int showDateId, int cinemasId, int filmId);
        ScheduleViewDTO GetScheduleById(int scheduleId);
        bool CreateSchedule(ScheduleDTO schedule);
        bool UpdateSchedule(int scheduleId, ScheduleDTO schedule);
        bool DeleteSchedule(int scheduleId);
        int Count();
        List<ScheduleViewDTO> Search(QueryObject query);

    }
}
