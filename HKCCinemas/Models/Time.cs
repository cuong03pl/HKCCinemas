using System.ComponentModel.DataAnnotations;

namespace HKCCinemas.Models
{
    public class Time
    {
        [Key]
        public int Id {  set; get; }
        public DateTime TimeValue { set; get; }
    }
}
