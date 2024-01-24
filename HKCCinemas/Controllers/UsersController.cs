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

        // GET: api/Users
        [HttpGet("getAllUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
          var data = _userRepo.GetAllUsers();
            return Ok(data);
        }

        // GET: api/Users/getUserById/5
        [HttpGet("getUserById/{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var data = _userRepo.GetUserById(id);
            return Ok(data);
        }
        [HttpGet("getUserByUserName/{username}")]
        public async Task<ActionResult<User>> GetUserByUserName(string username)
        {
            var data = _userRepo.GetUserByUserName(username);
            return Ok(data);
        }
        // PUT: api/Users/updateUser/5
        [HttpPut("updateUser/{id}")]
        public async Task<IActionResult> PutUser(int id, [FromBody] User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            try
            {
               _userRepo.UpdateUser(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                
            }

            return NoContent();
        }

        // POST: api/Users/createUser
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("createUser")]
        public async Task<ActionResult<User>> PostUser([FromBody] User user)
        {
          if (_context.User == null)
          {
              return Problem("Entity set 'CinemasContext.User'  is null.");
          }
            else
            {
                _userRepo.CreateUser(user);
                return Ok("Thêm thành công");
            }

           
        }

        // DELETE: api/Users/deleteUser/5
        [HttpDelete("deleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_userRepo.DeleteUser(id))
            {
                return Ok("Xóa thành công");
            }
            else return BadRequest();
        }

        // GET: api/Users/countUser
        [HttpGet("countUser")]
        public IActionResult GetCountUser()
        {
            int data = _userRepo.GetCountUser();
            return Ok(data);
        }
    }
}
