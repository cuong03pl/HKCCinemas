using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKCCinemas.Models
{
    public class ShowDate
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CinemasId { get; set; }

        [ForeignKey("CinemasId")]
        public Cinemas Cinemas { get; set; }
    }
}
