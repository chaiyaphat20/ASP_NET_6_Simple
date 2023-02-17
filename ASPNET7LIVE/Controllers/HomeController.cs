using ASPNET7LIVE.Services.ThaiDate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET7LIVE
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IThaiDate  _thaiDate;
        public HomeController(IThaiDate thaiDate) {  //Inject ผ่าน Interface
            _thaiDate = thaiDate;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var myThaiDate = _thaiDate.ShowThaiDate();
            return Ok(new {message = $"My Thai Date {myThaiDate}" });
        }
    }
}
