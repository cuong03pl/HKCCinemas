using System.ComponentModel.DataAnnotations.Schema;

namespace HKCCinemas.Models
{
    public class CategoryFilm
    {
        public int CategoryId { get; set; }
        public int FilmId { get; set; }

        public Film Film { get; set; }

        public Category Category { get; set; }

    }
}
