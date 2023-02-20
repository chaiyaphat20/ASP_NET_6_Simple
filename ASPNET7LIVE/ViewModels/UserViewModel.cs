using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNET7LIVE.ViewModels
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "ชื่อสกุลห้ามว่าง ห้ามว่าง")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "อีเมลล์ ห้ามว่าง")]
        [EmailAddress(ErrorMessage = "รูปแบบ อีเมลล์ไม่ถูกต้อง")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "รหัสผ่าน ห้ามว่าง")]
        [StringLength(100, ErrorMessage = "รหัสผ่านต้องมากกว่า {2}ตัว และไม่เกิน {1}ตัว", MinimumLength = 3)]
        public string Password { get; set; } = null!;
    }
}
