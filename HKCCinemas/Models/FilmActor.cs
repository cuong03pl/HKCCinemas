namespace HKCCinemas.Models
{
    public class FilmActor
    {
        public int filmId {  get; set; }
        public int actorId {  get; set; }

        public Film Film { get; set; }
        public Actor Actor { get; set; }

    }
}
