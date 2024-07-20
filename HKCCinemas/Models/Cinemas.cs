using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKCCinemas.Models
{
    public class Cinemas
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string? Image {  get; set; }
        public string? Background {  get; set; }

        public ICollection<ShowDate> ShowDates { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public int CinemasCategoryId { get; set; }

        [ForeignKey("CinemasCategoryId")]
        public CinemasCategory CinemasCategory { get; set; }

    }
}
