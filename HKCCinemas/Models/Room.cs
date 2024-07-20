using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKCCinemas.Models
{
    public class Room
    {
        [Key] 
        public int Id { get; set; }

        public string RoomName { get; set; }

        public int CinemasId {  get; set; }
        [ForeignKey("CinemasId")]
        public Cinemas Cinemas { get; set;}
        public ICollection<Seat> Seats { get;}

    }
}
