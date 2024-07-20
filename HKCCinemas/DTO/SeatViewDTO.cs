namespace HKCCinemas.DTO
{
    public class SeatViewDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public RoomDTO Room { get; set; }
        public CinemasDTO Cinemas { get; set; }
    }
}
