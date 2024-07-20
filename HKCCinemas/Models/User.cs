using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HKCCinemas.Models
{
    public class User: IdentityUser
    {
        [Required(ErrorMessage = "Vui lòng nhập tên người dùng.")]
        public override string UserName { get; set; }

        public string? Avatar { get; set; }

        public ICollection<BookingUser> BookingUsers { get; set; }


    }
}
