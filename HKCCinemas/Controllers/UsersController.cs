using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HKCCinemas.Models;
using HKCCinemas.Repo;
using HKCCinemas.Interfaces;
using HKCCinemas.Helper;

namespace HKCCinemas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly CinemasContext _context;
        private readonly IUserRepo _userRepo;

        public UsersController(CinemasContext context, IUserRepo userRepo)
        {
            _context = context;
            _userRepo = userRepo;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> getAllUsers()
        {
            var data = _userRepo.GetAllUsers();
            return Ok(data);
        }

        [HttpGet("getUserById/{id}")]
        public ActionResult<User> GetUserById(string id)
        {
            var data = _userRepo.GetUserById(id);
            return Ok(data);
        }

        [HttpGet("getUserByUserName/{username}")]
        public ActionResult<User> GetUserByUserName(string username)
        {
            var data = _userRepo.GetUserByUserName(username);
            return Ok(data);
        }
        [HttpGet("getCountUser")]
        public ActionResult<User> Count()
        {
            var data = _userRepo.Count();
            return Ok(data);
        }

        [HttpDelete("deleteUser/{id}")]
        public async Task<ActionResult<User>> DeleteUser(string id)
        {
            var data = await _userRepo.DeleteUser(id);
            if (data)
            {
                return Ok("Xóa người dùng thành công");
            }
            return BadRequest("Xóa người dùng thất bại ");
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] QueryObject query)
        {
            var data = _userRepo.Search(query);
            return Ok(data);

        }

    }
}
