using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace HKCCinemas.Models
{
    public class BookingUser
    {
        [Key]
        public int Id { get; set; }

        public string OrderCode { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [Display(Name = "Ngày đặt")]
        public DateTime? BookingDate { get; set; }
        public ICollection<BookingDetail> BookingDetails { get; set; }
    }
    
}
