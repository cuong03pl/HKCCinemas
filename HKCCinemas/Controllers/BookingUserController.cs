using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HKCCinemas.Models;
using HKCCinemas.DTO;
using HKCCinemas.Interfaces;
using HKCCinemas.Repo;
using System.Numerics;

namespace HKCCinemas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingUserController : ControllerBase
    {
        private readonly CinemasContext _context;
        private readonly IBookingUserRepo _bookingUserRepo;

        public BookingUserController(CinemasContext context, IBookingUserRepo bookingUserRepo)
        {
            _context = context;
            _bookingUserRepo = bookingUserRepo;
        }

        // GET: api/BookingDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingUserlViewDTO>>> GetBookingDetails()
        {
         var data = _bookingUserRepo.GetAllBookingUsers();
            return Ok(data);
        }


        [HttpGet("{userid}")]
        public async Task<ActionResult<IEnumerable<BookingDetailViewDTO>>> GetAllBookingDetailsByUserId(string userid)
        {
            var data = _bookingUserRepo.GetAllBookingUsersByUserId(userid);
            return Ok(data);
        }
        

        

        [HttpPost]
        public async Task<ActionResult<BookingDetail>> PostBookingDetail([FromForm] BookingUserDTO bookingDetail)
        {
            if (await _bookingUserRepo.CreateBookingUser(bookingDetail))
            {
                return Ok("Thêm thành công");
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/BookingDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingDetail(int id)
        {
            if (_bookingUserRepo.DeleteBookingUser(id))
            {
                return Ok("Xóa thành công");
            }
            else return BadRequest();
        }

        // DELETE: api/BookingDetails/5
        [HttpGet("GetCountTicket")]
        public async Task<IActionResult> GetCountTicket()
        {
            var count = _bookingUserRepo.GetCountTicket();
            return Ok(count);
        }


        // DELETE: api/BookingDetails/5
        [HttpGet("GetTop5FIlm")]
        public async Task<IActionResult> GetTop5FIlm()
        {
            var count = _bookingUserRepo.GetTop5Films();
            return Ok(count);
        }


        [HttpGet("GetTotalMoney")]
        public async Task<IActionResult> GetTotalMoney()
        {
            var count = _bookingUserRepo.GetTotalMoney();
            return Ok(count);
        }
    }
}
