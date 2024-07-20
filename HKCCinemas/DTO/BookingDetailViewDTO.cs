using HKCCinemas.Models;

namespace HKCCinemas.DTO
{
    public class BookingDetailViewDTO
    {
        public int Id { get; set; }
        public TicketDTO Ticket { get; set; }
        public SeatDTO Seat { get; set; }
        public ScheduleViewDTO Schedule { get; set; }
    }
}
