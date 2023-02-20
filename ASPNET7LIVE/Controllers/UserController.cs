using ASPNET7LIVE.Areas.Identity.Data;
using ASPNET7LIVE.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ASPNET7LIVE.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;  //ไว้ manage user
        private readonly SignInManager<User> _signInManager;  //ไว้ทำ login
        private readonly IWebHostEnvironment _hosting;  //ไว้ get path (เข้าถึ งwwwroot folder)
        public UserController(UserManager<User> userManager, SignInManager<User> signInManager,IWebHostEnvironment hosting) {
            _userManager = userManager;
            _signInManager = signInManager;
            _hosting = hosting;
        }

        // api/user/register
        [HttpPost]
        [RequestSizeLimit(10*1024*1024)] // 10 MB
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            var user = new User
            {
                FullName = register.FullName,
                Email = register.Email,
                UserName = register.Email
            };

            //upload file base64
            if(register.Photo != "nopic.png")
            {
                var  base64array = Convert.FromBase64String(register.Photo!);

                var newFileName = Guid.NewGuid() + ".png";  //Guid.NewGuid() random ไม่ให้ซ้ำ
                var imagePath = Path.Combine($"{_hosting.WebRootPath}/upload/{newFileName}");

                await System.IO.File.WriteAllBytesAsync(imagePath, base64array);
                user.Photo = newFileName;
            }

            var result =   await _userManager.CreateAsync(user,register.Password);
            if(result.Succeeded)
            {
                return  Created("", new {massage = "ลงทะเบียนสำเร็จ"});
            }
            return BadRequest(new {message = "เกิดข้อผิดพลาดมีผู้ใช้งานแล้ว"});
        }



        //Log In
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
            if (user == null) return NotFound(new { message = "ไม่พบผู้ใช้นี้ในระบบ" });

            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, false, false);
            if (result.Succeeded)
            {
                // สร้าง token
                return await CreateToken(loginViewModel.Email);
            }
            return Unauthorized(new { message = "อีเมล์หรือรหัสผ่านไม่ถูกต้อง" });
        }


        private async Task<IActionResult> CreateToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            //1. ค้นหาว่า user คนนี้ role อะไร ถ้าต้องการใช้ role-base authentication
            var roles = await _userManager.GetRolesAsync(user);

            if (user != null)
            {
                //  generate token that is valid for 7 days
                var tokenHandler = new JwtSecurityTokenHandler();
                var secretKey = Encoding.UTF8.GetBytes("odn051PvFMtRTBZsqmWkGJl8CHbKceQz"); //jwt secret key

                var subject = new List<Claim>
                 {
                    new Claim("user_id", user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                 };

                // 2.เพิ่ม role เข้าไปใน payload ถ้าต้องการใช้ role-base authentication
                subject.AddRange(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(subject),
                    Expires = DateTime.UtcNow.AddDays(7), //วันหมดอายุของ token
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var resultToken = new
                {
                    access_token = tokenHandler.WriteToken(token),
                    expiration = token.ValidTo
                };

                return Ok(resultToken);

            }
            return Unauthorized();
        }
    }
}
