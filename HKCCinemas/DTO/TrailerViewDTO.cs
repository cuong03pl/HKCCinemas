using HKCCinemas.Models;

namespace HKCCinemas.DTO
{
    public class TrailerViewDTO
    {
        public int Id { set; get; }
        public string? Link { set; get; }
        public int? FilmId { set; get; }
        public int Count { get; set; } = 0;

    }
}
