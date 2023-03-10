using ASPNET7LIVE.Data;
using ASPNET7LIVE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNET7LIVE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly APIContext _context;

        public CategoryController(APIContext context)
        {
            _context = context;
        }

        /*
        // GET ALL : api/Category
        // Task<ActionResult<IEnumerable<Category>>>
        [HttpGet]
        public async Task<IActionResult> GetCategory()
        {
            //AsNoTracking() ใช้เพื่อทำให้เร็วขึ้น กรณีที่เรา ไม่ได้เอา ตัวแปรไปใช้งาต่อ
            //ถ้าเรา เอาตัวแปร catgory ไปใช้งานต่อ ห้ามใช้
            //OrderByDescending(c=>c.CategoryId) คือต้องการเรียงโดยใช้ CategoryId จากมากไปน้อย
            var category = await _context.Category.OrderByDescending(c => c.CategoryId).AsNoTracking().ToListAsync();
            var categoryFromSQL = await _context.Category.FromSqlRaw("select * from Category order by CategoryId desc").ToListAsync();

            var total = await _context.Category.CountAsync();
            return Ok(new { totalRow = total, data = categoryFromSQL });
        }
        */

        //api/Category
        //api/category?page=1&pageSize=3
        [HttpGet]
        public async Task<IActionResult> GetCategoryPag(int page =1,int pageSize = 3)
        {
            var category = await _context.Category.
                OrderByDescending(c => c.CategoryId)
                .Skip((page-1) * pageSize ) //page-1 * pageSize คือ สูตรมาตฐานทำ pagination  -> page=1 skip ไป 0 หน้า 
                .Take(pageSize) //กำหนดว่าต้องการแสดงผล take = 3 นั้นคือ ให้แสดง 1 หน้า มี 3 record
                .AsNoTracking().ToListAsync();

            var total = await _context.Category.CountAsync();
            return Ok(new { totalRow = total, data = category }); ;
        }


        // GET BY ID: api/Category/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _context.Category.FindAsync(id);

            if (category == null)
            {
                return NotFound(new { message = "ไม่พบข้อมูลนี้ในระบบ" });
            }

            return Ok(category);
        }

        //Add Data
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            await _context.Category.AddAsync(category);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);  //call action  GetCategory(int id)

            return Created("", new { message = "เพิ่มข้อมูลสำเร็จ" });
        }

        //Delete Data
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Category.FindAsync(id);
            if(category == null)
            {
                return NotFound();
            }

            _context.Category.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //Update db
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if(id != category.CategoryId)
            {
                return BadRequest(new { message = "Id ที่จะแก้ไข ไม่ตรงกัน"});
            }

            _context.Entry(category).State = EntityState.Modified;  //โยนค่าที่จะแก้ไข ลง ที่ state แล้ว เปิดให้แก้ไข แล้ว save change
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.CategoryId == id);
        }
    }
}
