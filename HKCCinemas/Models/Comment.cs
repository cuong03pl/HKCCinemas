using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKCCinemas.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string Content { get; set; }

        public string UserID { get; set; }
        public int FilmId { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        [ForeignKey("FilmId")]
        public Film Film { get; set; }
    }
}
