using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HKCCinemas.Models;
using AutoMapper;
using HKCCinemas.Interfaces;
using HKCCinemas.DTO;
using HKCCinemas.Repo;
using HKCCinemas.Helper;

namespace HKCCinemas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly CinemasContext _context;
        private readonly IScheduleRepo _scheduleRepo;
        private readonly IMapper _mapper;


        public SchedulesController(CinemasContext context, IScheduleRepo scheduleRepo, IMapper mapper)
        {
            _context = context;
            _scheduleRepo = scheduleRepo;
            _mapper = mapper;
        }

        [HttpGet("getCount")]
        public async Task<ActionResult<int>> Count()
        {
            var data = _scheduleRepo.Count();
            return Ok(data);
        }
        // GET: api/Schedules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduleViewDTO>>> GetSchedules()
        {
            return Ok(_scheduleRepo.GetAllSchedule());
        }

        [HttpGet("GetAllScheduleByFilmIdAndRoomId/{filmId}/{roomId}")]
        public async Task<ActionResult<IEnumerable<ScheduleDTO>>> GetAllScheduleByFilmIdAndRoomId(int filmId, int roomId)
        {
            var scheduleMapper = _mapper.Map<List<ScheduleDTO>>(_scheduleRepo.GetAllScheduleByFilmIdAndRoomId(filmId, roomId));
            return Ok(scheduleMapper);
        }
        [HttpGet("GetAllScheduleByShowDateAndCinemas/{showDateId}/{cinemasId}")]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetAllScheduleByShowDateAndCinemas(int showDateId, int cinemasId)
        {
            var scheduleMapper = (_scheduleRepo.GetAllScheduleByShowDateAndCinemas(showDateId, cinemasId));
            return Ok(scheduleMapper);
        }

        [HttpGet("GetScheduleByShowDateAndCinemasAndFilm/{showDateId}/{cinemasId}/{filmId}")]
        public async Task<ActionResult<ScheduleDTO>> GetScheduleByShowDateAndCinemasAndFilm(int showDateId, int cinemasId, int filmId)
        {
            var scheduleMapper = (_scheduleRepo.GetScheduleByShowDateAndCinemasAndFilm(showDateId, cinemasId, filmId));
            return Ok(scheduleMapper);
        }
        // GET: api/Schedules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ScheduleViewDTO>> GetSchedule(int id)
        {
            var scheduleMapper = (_scheduleRepo.GetScheduleById(id));
            return Ok(scheduleMapper);
        }

        // PUT: api/Schedules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchedule(int id, [FromForm] ScheduleDTO schedule)
        {
            if(_scheduleRepo.UpdateSchedule(id, schedule))
            {
                return Ok("Sửa thành công");
            }
            else { return BadRequest("Sửa thất bại"); }
        }

        // POST: api/Schedules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostSchedule([FromForm] ScheduleDTO schedule)
        {
            if (_scheduleRepo.CreateSchedule(schedule))
            {
                return Ok("Thêm thành công");
            }
            else { return BadRequest("Thêm thất bại"); }
        }

        // DELETE: api/Schedules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            if (_scheduleRepo.DeleteSchedule(id))
            {
                return Ok("Xóa thành công");
            }
            else { return BadRequest("Xóa thất bại"); }
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] QueryObject query)
        {
            var data = _scheduleRepo.Search(query);
            return Ok(data);

        }
    }
}
