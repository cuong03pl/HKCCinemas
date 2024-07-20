using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HKCCinemas.Models;
using HKCCinemas.Interfaces;
using HKCCinemas.DTO;
using AutoMapper;

namespace HKCCinemas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatsController : ControllerBase
    {
        private readonly CinemasContext _context;
        private readonly ISeatRepo _seatRepo;
        private readonly IMapper _mapper;


        public SeatsController(CinemasContext context, ISeatRepo seatRepo, IMapper mapper)
        {
            _context = context;
            _seatRepo = seatRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeatViewDTO>>> GetSeats()
        {

            var data = _seatRepo.GetAllSeats();
            return Ok(data);
        }

        [HttpGet("/GetSeatById/{seatId}")]
        public async Task<ActionResult<IEnumerable<SeatViewDTO>>> GetSeatById(int seatId)
        {
            
            var data = _seatRepo.GetSeatById(seatId);
            return Ok(data);
        }

        [HttpGet("/GetSeatByRoomId/{roomId}/{cinemasId}")]
        public async Task<ActionResult<IEnumerable<SeatViewDTO>>> GetSeatsByRoomId(int roomId, int cinemasId)
        {
            var data = _seatRepo.GetSeatByRoomId(roomId, cinemasId);
            return Ok(data);
        }
        [HttpPost("/GetSeatsByIds")]
        public async Task<ActionResult<IEnumerable<SeatViewDTO>>> GetSeatsByIds([FromForm] int[] seatIds)
        {
            var data = _seatRepo.GetSeatsByIds(seatIds);
            return Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeat(int id, [FromForm] SeatDTO seat)
        {
            if (_seatRepo.UpdateSeat(id, seat))
            {
                return Ok("Sửa thành công");
            }
            else return BadRequest("Sửa thất bại");
        }

        
        [HttpPost]
        public async Task<ActionResult<Seat>> PostSeat([FromForm] SeatDTO seat)
        {
            if (_seatRepo.CreateSeat(seat))
            {
                return Ok("Tạo thành công");
            }
            else return BadRequest("Tạo thất bại");
        }

        // DELETE: api/Seats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeat(int id)
        {
            if (_seatRepo.DeleteSeat(id))
            {
                return Ok("Xóa thành công");
            }
            else return BadRequest("Xóa thất bại");
        }

        [HttpGet("isAvailable/{seatId}/{scheduleId}")]
        public async Task<IActionResult> IsAvailable(int seatId, int scheduleId)
        {
            if (_seatRepo.isAvailable(seatId, scheduleId))
            {
                return Ok(true);
            }
            else return Ok(false);
        }

    }
}
