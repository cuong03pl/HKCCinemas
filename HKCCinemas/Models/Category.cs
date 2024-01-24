using System.ComponentModel.DataAnnotations;

namespace HKCCinemas.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<CategoryFilm> categoryFilms { get; set; }

    }
}
