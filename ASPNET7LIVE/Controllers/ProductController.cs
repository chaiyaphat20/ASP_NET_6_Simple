using ASPNET7LIVE.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNET7LIVE.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly APIContext _context;

        public ProductController(APIContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Getproduct()
        {

            var product = await _context.Productc.OrderByDescending(c => c.ProductId).AsNoTracking().ToListAsync();
            var total = await _context.Productc.CountAsync();
            return Ok(new { toTALrOW = total, data = product });
        }
    }
}
