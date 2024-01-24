using System.ComponentModel.DataAnnotations.Schema;

namespace HKCCinemas.Models
{
    public class CategoryFilm
    {
        public int CategoryId { get; set; }
        public int FilmId { get; set; }

        [ForeignKey("FilmId")]
        public Film Film { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

    }
}
