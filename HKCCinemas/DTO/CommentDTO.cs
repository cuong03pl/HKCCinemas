namespace HKCCinemas.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string Content { get; set; }

        public int UserID { get; set; }
    }
}
