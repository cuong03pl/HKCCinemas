using HKCCinemas.Interfaces;
using HKCCinemas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HKCCinemas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepo accountRepo;

        public AccountController(IAccountRepo accountRepo) {
            this.accountRepo = accountRepo;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register( [FromForm] RegisterModel registerModel)
        {

            var result =await accountRepo.Register(registerModel);
            if (result.Succeeded)
            {
                return Ok("Đăng kí thành công");
            }
            else return Ok(result.Errors);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginModel loginModel)
        {
            var result = await accountRepo.Login(loginModel);
            if(string.IsNullOrEmpty(result)) return Unauthorized();
            return Ok(result);
        }

        [HttpGet("profile")]
        [Authorize] // Yêu cầu xác thực để truy cập API này
        public IActionResult GetUserProfile()
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            return Ok(new { UserName = userName });
        }

        
    }
}
