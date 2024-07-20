using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKCCinemas.Models
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int RoomId { get; set; }
        public int CinemasId { get; set; }
        public int ShowDateId { get; set; }


        public TimeSpan StartTime {  get; set; }
        public TimeSpan EndTime { get; set; }


        [ForeignKey("FilmId")]
        public Film Film { get; set; }

        [ForeignKey("CinemasId")]
        public Cinemas Cinemas { get; set; }
        [ForeignKey("RoomId")]
        public Room Room { get; set; }

        [ForeignKey("ShowDateId")]
        public ShowDate ShowDate { get; set; }

        public ICollection<SeatStatus> SeatStatuses { get; set; }
    }
}
