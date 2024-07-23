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
        [HttpGet("GetCinemasByCategoryId/{cinemasId}")]
        public async Task<ActionResult<IEnumerable<Cinemas>>> GetCinemasByCategoryId(int cinemasId)
        {
            var data = _cinemasRepo.GetCinemasByCategoryId(cinemasId);
            return Ok(data);
        }
        // GET: api/Cinemas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cinemas>> GetCinemas(int id)
        {
          var data = _cinemasRepo.GetCinemasById(id);
            return Ok(data);
        }

        [HttpGet("getCount")]
        public async Task<ActionResult<Cinemas>> CountCinemas()
        {
            var data = _cinemasRepo.CountCinemas();
            return Ok(data);
        }
        

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCinemas(int id, [FromForm] CinemasDTO cinemas)
        {
              if(  await _cinemasRepo.UpdateCinemas(id, cinemas))
                {
                    return Ok("Sửa thành công");
                }
              else
                {
                    return BadRequest();
                }
        }

      
        [HttpPost]
        public async Task<ActionResult<Cinemas>> PostCinemas([FromForm] CinemasDTO cinemas)
        {
            if (await _cinemasRepo.CreateCinemasAsync(cinemas))
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
                return Ok("Xóa rạp thành công");
            }
            else return BadRequest();
        }

        [HttpGet("search/{keyword}")]
        public async Task<IActionResult> SearchCinemas(string keyword)
        {
            var data = _cinemasRepo.SearchCinemas(keyword);
            return Ok(data);

        }

    }
}
