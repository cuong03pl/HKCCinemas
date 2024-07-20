namespace HKCCinemas.DTO
{
    public class TicketViewDTO
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public int Price { get; set; }
        public string CinemasName { get; set; }
        public string FilmName { get; set; }
        public DateTime ShowDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
