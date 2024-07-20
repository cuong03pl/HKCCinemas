using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKCCinemas.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public int Price { get; set; }
        public int ScheduleId { get; set; }

        [ForeignKey("ScheduleId")]
        public Schedule Schedule {  get; set; }

        public ICollection<BookingDetail> BookingDetails { get; set; }

    }
}
