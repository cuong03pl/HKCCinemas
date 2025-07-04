﻿using System;
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
using HKCCinemas.Helper;

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
            var data = _filmRepo.GetAllFilms();
            return Ok(data);

        }

        [HttpGet("getTop5")]
        public async Task<ActionResult<IEnumerable<Film>>> GetTop5Films()
        {
            var data = _mapper.Map<List<FilmDTO>>(_filmRepo.GetTop5Films());
            return Ok(data);

        }

        
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Film>>> GetAllFilmByQuery([FromQuery] QueryObject query)
        {
            var data = _filmRepo.GetAllFilmsByQuery(query);
            return Ok(data);

        }

        [HttpGet("GetAllFilmByCategory/{id}")]
        public async Task<ActionResult<IEnumerable<Film>>> GetAllFilmByCategory(int id)
        {
            var data = _mapper.Map<List<FilmDTO>>(_filmRepo.GetAllFilmByCategory(id));
            return Ok(data);

        }

        [HttpGet("GetAllFilmByActor/{actorId}")]
        public async Task<ActionResult<IEnumerable<int>>> GetAllFilmByActor(int actorId)
        {
            var data = _filmRepo.GetAllFilmByActor(actorId);
            return Ok(data);

        }

        // GET: api/Films/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Film>> GetFilm(int id)
        {
            var data = _mapper.Map<FilmDTO>(_filmRepo.GetFilmById(id));
            return Ok(data);
        }

        [HttpGet("getNowShowingFilms")]
        public async Task<ActionResult<Film>> GetNowShowingFilms()
        {
            var data = _mapper.Map<List<FilmDTO>>(_filmRepo.GetNowShowingFilms());
            return Ok(data);
        }
        
        [HttpGet("getUpcomingFilms")]
        public async Task<ActionResult<Film>> GetUpcomingFilms()
        {
            var data = _mapper.Map<List<FilmDTO>>(_filmRepo.GetUpcomingFilms());
            return Ok(data);
        }
        [HttpGet("getCount")]
        public async Task<ActionResult<int>> Count()
        {
            var data = _filmRepo.Count();
            return Ok(data);
        }

        
        [HttpGet("getSimilarFilm/{categoryId}")]
        public async Task<ActionResult<Film>> GetSimilarFilms(int categoryId)
        {
            var data = _mapper.Map<List<FilmDTO>>(_filmRepo.GetSimilarFilm(categoryId));
            return Ok(data);
        }
        // PUT: api/Films/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilm(int id, [FromForm] FilmDTO film)
        {
            
            if(await _filmRepo.UpdateFilmAsync(id, film))
            {
                return Ok("Cập nhật phim thành công");
            }
            else return BadRequest( "Cập nhật phim thất bại");
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
