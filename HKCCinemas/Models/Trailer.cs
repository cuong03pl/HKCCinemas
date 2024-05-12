using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKCCinemas.Models
{
    public class Trailer
    {
        [Key]
        public int Id { set; get; }
        public string? Link { set; get; }
        public int? FilmId { set; get; }

        [ForeignKey("FilmId")]
        public Film Film { set; get; }

    }
}
