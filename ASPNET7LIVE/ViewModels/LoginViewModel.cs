using System.ComponentModel.DataAnnotations;

namespace ASPNET7LIVE.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "อีเมลล์ ห้ามว่าง")]
        [EmailAddress(ErrorMessage = "รูปแบบ อีเมลล์ไม่ถูกต้อง")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "รหัสผ่าน ห้ามว่าง")]
        [StringLength(100, ErrorMessage = "รหัสผ่านต้องมากกว่า {2}ตัว และไม่เกิน {1}ตัว", MinimumLength = 3)]
        public string Password { get; set; } = null!;

    }
}
