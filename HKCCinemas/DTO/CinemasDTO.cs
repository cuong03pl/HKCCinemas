namespace HKCCinemas.DTO
{
    public class CinemasDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string? Image { get; set; }
        public IFormFile? formFile { get; set; }
    }
}
