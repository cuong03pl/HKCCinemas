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
    public class CinemasCategoriesController : ControllerBase
    {
        private readonly CinemasContext _context;
        private readonly ICinemasCategoryRepo _cinemasCategoryRepo;
        private readonly IMapper _mapper;
        public CinemasCategoriesController(IMapper mapper,CinemasContext context, ICinemasCategoryRepo cinemasCategoryRepo)
        {
            _context = context;
            _cinemasCategoryRepo = cinemasCategoryRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CinemasCategoryDTO>>> GetCinemasCategories()
        {
            var data = _cinemasCategoryRepo.GetCinemasCategories();
            var categoryMapper = _mapper.Map<List<CinemasCategoryDTO>>(data);
            return Ok(categoryMapper);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CinemasCategoryDTO>> GetCinemasCategory(int id)
        {
          
            var cinemasCategory = _cinemasCategoryRepo.GetCinemasCategory(id);
            var categoryMapper = _mapper.Map<CinemasCategoryDTO>(cinemasCategory);
            return categoryMapper;
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCinemasCategory(int id,[FromForm] CinemasCategoryDTO cinemasCategory)
        {
           if(await _cinemasCategoryRepo.UpdateCategoryCinemas(id, cinemasCategory))
            {
                return Ok("Sửa thành công");
            }
           else return BadRequest();
        }

        
        [HttpPost]
        public async Task<ActionResult<CinemasCategory>> PostCinemasCategory([FromForm] CinemasCategoryDTO cinemasCategory)
        {
            if (await _cinemasCategoryRepo.CreateCinemasCategory(cinemasCategory))
            {
                return Ok("Thêm thành công");
            }
            else  return BadRequest();
           
        }

        // DELETE: api/CinemasCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCinemasCategory(int id)
        {
            if (_cinemasCategoryRepo.DeleteCinemasCategory(id))
            {
                return Ok("Xóa thành công");
            }
            else return BadRequest();
        }

        
    }
}
