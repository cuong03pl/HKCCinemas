namespace HKCCinemas.DTO
{
    public class ShowDateViewDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
       public CinemasDTO Cinemas { get; set; }

        public int Count { get; set; } = 0;
    }
}
