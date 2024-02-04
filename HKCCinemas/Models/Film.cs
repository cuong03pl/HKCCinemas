using System.ComponentModel.DataAnnotations;

namespace HKCCinemas.Models
{
    public class Film
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public string Synopsis { get; set; }
        public int AgeLimit { get; set; }
        public int Duration { get; set; }
        public string Country { get; set; }
        public double Rating { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Thumbnail { get; set; }
        public string Director { get; set; }
        public ICollection<FilmActor> filmActors { get; set; }
        public ICollection<CategoryFilm> categoryFilms { get; set; }
        public ICollection<Comment> comments { get; set; }


    }

}
