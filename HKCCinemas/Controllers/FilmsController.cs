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
using AutoMapper;
using HKCCinemas.DTO;

namespace HKCCinemas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private readonly CinemasContext _context;
        private readonly IFilmRepo _filmRepo;
        private readonly IMapper _mapper;

        public FilmsController(CinemasContext context, IFilmRepo filmRepo, IMapper mapper)
        {
            _context = context;
            _filmRepo = filmRepo;
            _mapper = mapper;
        }

        // GET: api/Films
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Film>>> GetAllFilms()
        {
            var data = _mapper.Map<List<FilmDTO>>(_filmRepo.GetAllFilms());
            return Ok(data);

        }

        // GET: api/Films/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Film>> GetFilm(int id)
        {
            var data = _mapper.Map<FilmDTO>(_filmRepo.GetFilmById(id));
            return Ok(data);
        }

        // PUT: api/Films/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilm(int id, [FromForm] FilmDTO film)
        {
            if (id != film.Id)
            {
                return BadRequest();
            }
            try
            {
               await _filmRepo.UpdateFilmAsync(film);
                return Ok("Cập nhật phim thành công");
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return NoContent();
        }

        // POST: api/Films
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Film>> PostFilm([FromForm] FilmDTO film)
        {
            if (await _filmRepo.CreateFilmAsync(film))
            {
                return Ok("Thêm thành công");
            }
            else return BadRequest();
        }

        // DELETE: api/Films/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            if (_filmRepo.DeleteFilm(id))
            {
                return Ok("Xóa thành công");
            }
            else return BadRequest();
        }
    }
       
}
