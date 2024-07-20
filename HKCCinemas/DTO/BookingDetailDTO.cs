using HKCCinemas.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKCCinemas.DTO
{
    public class BookingDetailDTO
    {
        public int Id { get; set; }

        public int TicketId { get; set; }

    }
}
