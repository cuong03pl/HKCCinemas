using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HKCCinemas.Models;
using HKCCinemas.DTO;
using AutoMapper;
using HKCCinemas.Interfaces;

namespace HKCCinemas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrailersController : ControllerBase
    {
        private readonly CinemasContext _context;
        private readonly ITrailerRepo _trailerRepo;
        private readonly IMapper _mapper;

        public TrailersController(CinemasContext context, ITrailerRepo trailerRepo, IMapper mapper)
        {
            _context = context;
            _trailerRepo = trailerRepo;
            _mapper = mapper;
        }

        // GET: api/Trailers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrailerDTO>>> GetAllTrailer()
        {
            var data = _mapper.Map<List<TrailerDTO>>(_trailerRepo.GetAllTrailer());
            return Ok(data);
        }

        [HttpGet("{filmid}")]
        public async Task<ActionResult<IEnumerable<TrailerDTO>>> GetAllTrailerByFilmId(int filmid)
        {
            var data = _mapper.Map<List<TrailerDTO>>(_trailerRepo.GetAllTrailerByFilmId(filmid));
            return Ok(data);
        }

        // PUT: api/Trailers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrailerAsync(int id, [FromForm] TrailerDTO trailer)
        {
           if(await _trailerRepo.UpdateTrailerAsync(id, trailer))
            {
                return Ok("Sủa thành công");
            }
           else return BadRequest();
        }

      
        [HttpPost]
        public async Task<ActionResult<Trailer>> PostTrailer([FromForm] TrailerDTO trailer)
        {
            if (await _trailerRepo.CreateTrailerAsync(trailer))
            {
                return Ok("Thêm thành công");
            }
            else  return BadRequest();
        }

        // DELETE: api/Trailers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrailer(int id)
        {
            if (_trailerRepo.DeleteTrailer(id))
            {
                return Ok("Xoá thành công");
            }
            else return BadRequest();
        }
    }
}
