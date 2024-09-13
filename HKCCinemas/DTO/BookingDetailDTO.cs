using HKCCinemas.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKCCinemas.DTO
{
    public class BookingDetailDTO
    {
        public int Id { get; set; }
        public TicketDTO Ticket { get; set; }
        public SeatDTO Seat { get; set; }
        public ScheduleViewDTO Schedule { get; set; }
        public User User { get; set; }
        public int Count { get; set; } = 0;

    }
}
