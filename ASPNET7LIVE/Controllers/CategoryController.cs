using ASPNET7LIVE.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET7LIVE.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly APIContext _context;

        public CategoryController(APIContext context)
        {
            _context = context;
        }
        // GET: api/Category/5
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
    }
}
