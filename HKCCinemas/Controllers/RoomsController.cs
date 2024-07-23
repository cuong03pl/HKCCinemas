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
using HKCCinemas.Repo;

namespace HKCCinemas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly CinemasContext _context;
        private readonly IRoomRepo _roomRepo;
        private readonly IMapper _mapper;
        public RoomsController(CinemasContext context, IRoomRepo roomRepo, IMapper mapper)
        {
            _context = context;
            _roomRepo = roomRepo;
            _mapper = mapper;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomViewDTO>>> GetRooms()
        {
            var data = _roomRepo.GetRooms();
            return Ok(data);
        }
        [HttpGet("GetRoomByCinemasId/{cinemasId}")]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRoomByCinemasId(int cinemasId)
        {
            var data = _mapper.Map<List<RoomDTO>>(_roomRepo.GetRoomByCinemasId(cinemasId));
            return Ok(data);
        }
        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDTO>> GetRoom(int id)
        {
            var data = _mapper.Map<RoomDTO>(_roomRepo.GetRoomById(id));
            return Ok(data);
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, [FromForm] RoomDTO room)
        {
            if(_roomRepo.updateRoom(id, room))
            {
                return Ok("Sửa thành công ");
            }
            else
            {
                return BadRequest("Sửa không thành công");
            }
        }

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom([FromForm] RoomDTO room)
        {
            if (_roomRepo.createRoom(room))
            {
                return Ok("Thêm thành công");
            }
            else return BadRequest("Thêm không thành công");
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            if (_roomRepo.deleteRoom(id))
            {
                return Ok("Xóa thành công");
            }
            else return BadRequest("Xóa không thành công");
        }
        [HttpGet("isCinemaRoomOccupied")]
        public async Task<IActionResult> isCinemaRoomOccupied([FromQuery] ScheduleDTO data)
        {
            if( _roomRepo.isCinemaRoomOccupied(data.CinemasId, data.FilmId, data.RoomId, data.ShowDateId, data.StartTime))
            {

            return Ok(true);
            }
            else return Ok(false);
        }

        [HttpGet("search/{keyword}")]
        public async Task<IActionResult> Search(string keyword)
        {
            var data = _roomRepo.Search(keyword);
            return Ok(data);

        }

    }
}
