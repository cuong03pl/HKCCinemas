namespace HKCCinemas.DTO
{
    public class ScheduleDTO
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int RoomId { get; set; }
        public int ShowDateId { get; set; }
        public int CinemasId { get; set; }
        public TimeSpan StartTime { get; set; }
    }
}
