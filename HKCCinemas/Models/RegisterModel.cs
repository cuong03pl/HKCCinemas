using System.ComponentModel.DataAnnotations;

namespace HKCCinemas.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên người dùng.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Tên người dùng chỉ được chứa các chữ cái và số.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [EmailAddress(ErrorMessage = "Vui lòng nhập đúng định dạng email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [MinLength(6, ErrorMessage = "Tên người dùng phải có ít nhất 6 ký tự.")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$", ErrorMessage = "Mật khẩu phải chứa ít nhất một chữ cái, một số và một ký tự đặc biệt.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu xác nhận.")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$", ErrorMessage = "Mật khẩu phải chứa ít nhất một chữ cái, một số và một ký tự đặc biệt.")]
        [Compare("Password", ErrorMessage = "Mật khẩu và mật khẩu xác nhận không khớp.")]
        public string? ConfirmPassword { get; set; }
    }
}
