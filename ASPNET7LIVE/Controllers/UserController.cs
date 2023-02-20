using ASPNET7LIVE.Areas.Identity.Data;
using ASPNET7LIVE.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET7LIVE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User>  _userManager;  //ไว้ manage user
        private readonly SignInManager<User>  _signInManager;  //ไว้ทำ login
        public UserController(UserManager<User> userManager, SignInManager<User> signInManager) {
            _userManager = userManager;
            _signInManager= signInManager;
        }

        // api/user/register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            var user = new User
            {
                FullName = register.FullName,
                Email = register.Email,
                UserName = register.Email
            };

            var result =   await _userManager.CreateAsync(user,register.Password);
            if(result.Succeeded)
            {
                return  Created("", new {massage = "ลงทะเบียนสำเร็จ"});
            }
            return BadRequest(new {message = "เกิดข้อผิดพลาดมีผู้ใช้งานแล้ว"});
        }
    }
}
