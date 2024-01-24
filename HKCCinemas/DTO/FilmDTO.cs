using System.ComponentModel.DataAnnotations;

namespace HKCCinemas.DTO
{
    public class FilmDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public string Synopsis { get; set; }
        public int AgeLimit { get; set; }
        public int Duration { get; set; }
        public int Country { get; set; }
        public double Rating { get; set; }
        public int Status { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Thumbnail { get; set; }
        public string Director { get; set; }
    }
}
