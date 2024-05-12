using HKCCinemas.Models;

namespace HKCCinemas.DTO
{
    public class TrailerDTO
    {
        public int Id { set; get; }
        public string? Link { set; get; }
        public int? FilmId { set; get; }
    }
}
