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

namespace HKCCinemas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CinemasContext _context;
        private readonly ICategoryRepo _categoryRepo;
        private readonly IMapper _mapper;


        public CategoriesController(CinemasContext context, ICategoryRepo categoryRepo, IMapper mapper)
        {
            _context = context;
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        // GET: api/Categories
        [HttpGet("getAllCategoriesByFilmId/{filmId}")]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategoryByFilmId(int filmId)
        {
            var data = _categoryRepo.GetAllCategoriesByFilmId(filmId);
            return Ok(data);
        }
        [HttpGet("getAllCategories")]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategory()
        {
            var data = _categoryRepo.GetAllCategories();
            return Ok(data);
        }
        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
         var data = _categoryRepo.GetCategoryById(id);
            return Ok(data);
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               
            }

            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory( [FromBody] CategoryDTO category)
        {
            var categoryMapper = _mapper.Map<Category>(category);
            if (_categoryRepo.CreateCategory( categoryMapper))
            {
                return Ok("Thêm thành công");
            }
            else return BadRequest();
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (_categoryRepo.DeleteCategory(id))
            {
                return Ok("xóa thanhf công ");
            }
            else return BadRequest();
        }

       
    }
}
