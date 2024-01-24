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

namespace HKCCinemas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemasController : ControllerBase
    {
        private readonly CinemasContext _context;
        private readonly ICinemasRepo _cinemasRepo;
        private readonly IMapper _mapper;

        public CinemasController(CinemasContext context, ICinemasRepo cinemasRepo, IMapper mapper)
        {
            _context = context;
            _cinemasRepo = cinemasRepo;
            _mapper = mapper;
        }

        // GET: api/Cinemas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cinemas>>> GetCinemas()
        {
         var data = _cinemasRepo.GetAllCinemas();
            return Ok(data);
        }

        // GET: api/Cinemas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cinemas>> GetCinemas(int id)
        {
          var data = _cinemasRepo.GetCinemasById(id);
            return Ok(data);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCinemas(int id, Cinemas cinemas)
        {
            if (id != cinemas.Id)
            {
                return BadRequest();
            }
            try
            {
                _cinemasRepo.UpdateCinemas(cinemas);
                return Ok("Sua thanh cong");
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return NoContent();
        }

      
        [HttpPost]
        public async Task<ActionResult<Cinemas>> PostCinemas([FromBody] Cinemas cinemas)
        {
            if (_cinemasRepo.CreateCinemas(cinemas))
            {
                return Ok("Thêm rạp thành công");
            }
            else return BadRequest();
        }

        // DELETE: api/Cinemas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCinemas(int id)
        {
            if (_cinemasRepo.DeleteCinemas(id))
            {
                return Ok("Xóa thành công");
            }
            else return BadRequest();
        }

        
    }
}
