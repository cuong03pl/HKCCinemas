using HKCCinemas.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKCCinemas.DTO
{
    public class BookingUserDTO
    {
        public int Id { get; set; }
        public string OrderCode { get; set; }
        public string UserId { get; set; }
        public int TicketId { get; set; }
        public DateTime? BookingDate { get; set; }
        public List<int> SeatIds { get; set; }

    }
}
