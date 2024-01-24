using System.ComponentModel.DataAnnotations;

namespace HKCCinemas.Models
{
    public class Cinemas
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

    }
}
