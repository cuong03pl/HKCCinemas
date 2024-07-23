using HKCCinemas.DTO;
using HKCCinemas.Models;

namespace HKCCinemas.Interfaces
{
    public interface IScheduleRepo
    {
        List<ScheduleViewDTO> GetAllSchedule ();
        List<ScheduleViewDTO> GetAllScheduleByFilmIdAndRoomId(int filmId, int roomId);
        List<ScheduleViewDTO> GetAllScheduleByShowDateAndCinemas(int showDateId, int cinemasId);
        ScheduleDTO GetScheduleByShowDateAndCinemasAndFilm(int showDateId, int cinemasId, int filmId);
        ScheduleViewDTO GetScheduleById(int scheduleId);

        bool CreateSchedule(ScheduleDTO schedule);
        bool UpdateSchedule(int scheduleId, ScheduleDTO schedule);
        bool DeleteSchedule(int scheduleId);

        List<ScheduleViewDTO> Search(string keyword);

    }
}
