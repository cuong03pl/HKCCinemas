namespace HKCCinemas.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        public string Content { get; set; }

        public string UserID { get; set; }
        public int FilmId { get; set; }
    }
}
