using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNET7LIVE.Models
{
    public class Productc
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }


        [Required(ErrorMessage = "ชื่อสินค้าห้ามว่าง")]
        public string ProductName { get; set; } = null!;

        [Required(ErrorMessage = "ราคา หเามว่าง")]
        [Range(0.00, 999999999.99, ErrorMessage = "ข้อมูลราคาไม่ถูกต้อง")]
        [Column(TypeName = "decimal(10,2)")] // 8 หลัก ทศนิยม 2 ตำแหน่ง  ตามหลัก sql server
        public decimal ProductPrice { get; set; }

        [Column(TypeName = "datetime")] //column type sql server
        public DateTime ProductExpire { get; set; } = DateTime.Now.AddHours(3); //default value เป็น 3 เดือน ข้างหน้า

        //FK
        [Required(ErrorMessage ="รหัสประเภท สินค้าห้ามว่าง")]
        public int CategaryId { get; set; }
    }
}
