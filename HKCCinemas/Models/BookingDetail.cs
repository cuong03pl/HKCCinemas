using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKCCinemas.Models
{
    public class BookingDetail
    {
        [Key]
        public int Id { get; set; }

        public int TicketId { get; set; }
        public int SeatId { get; set; }
        public int BookingUserId { get; set; }

        [ForeignKey("TicketId")]
        public Ticket Ticket  { get; set; }

        [ForeignKey("SeatId")]
        public Seat Seat { get; set; }

        [ForeignKey("BookingUserId")]
        public BookingUser BookingUser { get; set; }
    }
}
