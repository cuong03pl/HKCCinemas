                                                                                namespace HKCCinemas.DTO
{
    public class CinemasDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string? Image { get; set; }
        public string? Background { get; set; }
        public int CinemasCategoryId { get; set; }
        public IFormFile? formFileImage { get; set; }
        public IFormFile? formFileBackground { get; set; }
    }
}
