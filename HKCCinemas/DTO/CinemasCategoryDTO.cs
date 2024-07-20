namespace HKCCinemas.DTO
{
    public class CinemasCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public IFormFile? formFile { get; set; }
    }
}
