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
        private readonly IRoleRepo _roleRepo;
        private readonly CinemasContext context;

        public AccountController(IAccountRepo accountRepo, CinemasContext context, IRoleRepo roleRepo) {
            this.accountRepo = accountRepo;
            this.context = context;
            _roleRepo = roleRepo;
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
        [Authorize] 
        public IActionResult GetUserProfile()
        {
            var userId = User.FindFirst(ClaimTypes.Name)?.Value;
            var user = context.Users.Where(u => u.Id == userId).FirstOrDefault();
            return Ok(user);
        }


        [HttpPost("createRole")]
        public async Task<IActionResult> CreateRole([FromForm] string rolename)
        {
            if (await _roleRepo.CreateRole(rolename))
            {
                return Ok("Thêm role thành công");
            }
            else return BadRequest();
        }

        [HttpGet("getRoles")]
        public async Task<IActionResult> GetRoles()
        {
           return Ok(_roleRepo.GetRoles());
        }

        [HttpPost("getRolesByUser/{userId}")]
        public async Task<IActionResult> GetRolesByUser(string userId)
        {
            var data = await _roleRepo.GetRolesByUser(userId);
            return Ok(data);
        }


        [HttpDelete("deleteRole/{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            if (await _roleRepo.DeleteRole(id))
            {
                return Ok("Xóa role thành công");
            }
            else return BadRequest();
        }
        [HttpPut("updateRole/{id}")]
        public async Task<IActionResult> UpdateRole(string id, [FromForm] string roleName)
        {
            if (await _roleRepo.UpdateRole(id, roleName))
            {
                return Ok("Cập nhật role thành công");
            }
            else return BadRequest();
        }


        [HttpPost("setRole/{userId}")]
        public async Task<IActionResult> SetRole(string userId, [FromForm] List<string> roles)
        {
            if (await _roleRepo.SetRole(userId, roles))
            {
                return Ok("Set role thành công");
            }
            else return BadRequest();
        }
        

    }
}
