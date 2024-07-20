using System.ComponentModel.DataAnnotations.Schema;

namespace HKCCinemas.Models
{
    public class SeatStatus
    {
        public int Id { get; set; }
        public int SeatId { get; set; }
        public int ScheduleId { get; set; }
        public int Status {  get; set; }

        [ForeignKey("SeatId")]
        public Seat Seat { get; set; }

        [ForeignKey("ScheduleId")]
        public Schedule Schedule { get; set; }
    }
}
