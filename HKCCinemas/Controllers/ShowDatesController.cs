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
using System.Numerics;
using HKCCinemas.Helper;

namespace HKCCinemas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowDatesController : ControllerBase
    {
        private readonly CinemasContext _context;
        private readonly IMapper _mapper;
        private readonly IShowDateRepo _showDateRepo;

        public ShowDatesController(CinemasContext context, IMapper mapper, IShowDateRepo showDateRepo)
        {
            _context = context;
            _mapper = mapper;
            _showDateRepo = showDateRepo;
        }

        [HttpGet("getCount")]
        public async Task<ActionResult<int>> Count()
        {
            var data = _showDateRepo.Count();
            return Ok(data);
        }

        // GET: api/ShowDates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShowDateViewDTO>>> GetShowDates()
        {
          var data  =  _showDateRepo.GetAllShowDate();
            return Ok(data);
        }
        [HttpGet("GetAllShowDateByCinemasId/{cinemasId}")]
        public async Task<ActionResult<IEnumerable<ShowDateDTO>>> GetAllShowDateByCinemasId(int cinemasId)
        {
            var data = _mapper.Map<List<ShowDateDTO>>(_showDateRepo.GetAllShowDateByCinemasId(cinemasId));
            return Ok(data);
        }
        // GET: api/ShowDates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShowDateDTO>> GetShowDate(int id)
        {
            var data = _mapper.Map<ShowDateDTO>(_showDateRepo.GetShowDateById(id));
            return Ok(data);
        }

        // PUT: api/ShowDates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShowDate(int id, [FromForm] ShowDateDTO showDate)
        {
            if (_showDateRepo.UpdateShowDate(id, showDate))
            {
                return Ok("Cập nhật thành công");
            }
            else return BadRequest("Cập nhật thất bại");
        }

        // POST: api/ShowDates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShowDate>> PostShowDate([FromForm] ShowDateDTO showDate)
        {
            if (_showDateRepo.CreateShowDate(showDate))
            {
                return Ok("Thêm thành công");
            }
            else return BadRequest("Thêm thất bại");
        }

        // DELETE: api/ShowDates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShowDate(int id)
        {
            if (_showDateRepo.DeleteShowDate(id))
            {
                return Ok("Xóa thành công");
            }
            else return BadRequest("Xóa thất bại");
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] QueryObject query)
        {
            var data = _showDateRepo.Search(query);
            return Ok(data);

        }

    }
}
