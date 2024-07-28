using HKCCinemas.Models;

namespace HKCCinemas.DTO
{
    public class ScheduleViewDTO
    {
        public int Id { get; set; }
        public FilmDTO Film { get; set; }
        public RoomDTO Room { get; set; }
        public ShowDateDTO ShowDate { get; set; }
        public CinemasDTO Cinemas { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int Count { get; set; } = 0;

    }
}
