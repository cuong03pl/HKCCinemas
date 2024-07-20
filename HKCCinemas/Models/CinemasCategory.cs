using System.ComponentModel.DataAnnotations;

namespace HKCCinemas.Models
{
    public class CinemasCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }

    }
}
