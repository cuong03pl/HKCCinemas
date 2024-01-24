using System.ComponentModel.DataAnnotations;

namespace HKCCinemas.Models
{
    public class Actor
    {
        [Key]
        public int Id { set; get; }

        [Display(Name = "Tên diễn viên")]
        public string Name { set; get; }

        [Display(Name = "Tên trong bộ phim")]
        public string RoleName { set; get; } // Tên trong bộ phim 

        [Display(Name = "Ảnh diễn viên")]
        public string Image { set; get; }

        public ICollection<FilmActor> filmActors { get; set; }


    }
}
