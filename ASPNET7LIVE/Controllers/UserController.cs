using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET7LIVE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController() { 
        
        }

        // api/user/register
        [HttpPost]
        public async Task<IActionResult> Register()
        {
            return Ok();
        }
    }
}
