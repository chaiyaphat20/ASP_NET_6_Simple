using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNET7LIVE.Models
{
    //[Table("categories")]
    public class Category
    {
        //[Column("cat_id")] //Mapping Column ตัว cat_id คือ column ในฐานข้อมูล ส่วน CategoryId คือ ค่าบน code จริง
        //[Key]  //PrimaryKey
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //auto running number
        public int CategoryId { get; set; }



        //[Column(TypeName = "ntext")]
        //FooBar fooBar = null!; Define as non-nullable, but tell compiler to ignore warning
        //FooBar? fooBar = null;  Define as nullable
        [Required(ErrorMessage ="ชื่อประเภท สินค้าห้ามว่าง")]
        public string CategoryName { get; set; } = null!;   // null!  ห้าม null หรือ none-nullable คือ CategoryName ต้องมีข้อมูล

        public bool IsActive { get; set; } = true;
    }
}
