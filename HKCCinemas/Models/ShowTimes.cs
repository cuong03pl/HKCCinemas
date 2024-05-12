using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKCCinemas.Models
{
    public class ShowTimes
    {
        [Key]
        public int Id { get; set; }
        public int CinemasId { get; set; }
        public int FilmId { get; set; }
        public int TimeId { get; set; }
        [ForeignKey("FilmId")]
        public Film Film { get; set; }

        [ForeignKey("CinemasId")]
        public Cinemas Cinemas { get; set; }
        [ForeignKey("TimeId")]
        public Time Time { get; set; }
    }
}
