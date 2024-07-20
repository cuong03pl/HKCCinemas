using System.ComponentModel.DataAnnotations.Schema;

namespace HKCCinemas.Models
{
    public class Seat
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int RoomID { get; set; }

        [ForeignKey("RoomID")]
        public Room Room { get; set; }

        public ICollection<BookingDetail> BookingDetails { get; set; }
        public ICollection<SeatStatus> SeatStatuses { get; set; }
    }
}
