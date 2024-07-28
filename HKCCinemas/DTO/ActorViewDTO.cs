using System.ComponentModel.DataAnnotations;

namespace HKCCinemas.DTO
{
    public class ActorViewDTO
    {
        public int Id { set; get; }

        [Display(Name = "Tên diễn viên")]
        public string Name { set; get; }

        [Display(Name = "Ảnh diễn viên")]
        public string? Image { set; get; }

        public int Count { get; set; } = 0;
    }
}
