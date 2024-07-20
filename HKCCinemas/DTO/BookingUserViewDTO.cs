using HKCCinemas.Models;

namespace HKCCinemas.DTO
{
    public class BookingUserlViewDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ICollection<BookingDetailViewDTO> Detail { get; set; }
    }
}
