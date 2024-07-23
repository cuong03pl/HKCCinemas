using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HKCCinemas.Models;
using HKCCinemas.Repo;
using HKCCinemas.DTO;
using AutoMapper;
using HKCCinemas.Interfaces;

namespace HKCCinemas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly CinemasContext _context;
        private readonly IActorRepo _actorRepo;
        private readonly IMapper _mapper;


        public ActorsController(CinemasContext context, IActorRepo actorRepo, IMapper mapper)
        {
            _context = context;
            _actorRepo = actorRepo;
            _mapper = mapper;
        }

        // GET: api/Actors
        [HttpGet("getActorByFimlId/{film_id}")]
        public  IActionResult GetActorByFimlId(int film_id)
        {
            var data = _mapper.Map<List<ActorDTO>> (_actorRepo.GetAllActorsByFilmId(film_id));
            return Ok(data);
        }

        // GET: api/Actors
        [HttpGet("getAllActors")]
        public IActionResult GetAllActors()
        {
            var data = _mapper.Map<List<ActorDTO>>(_actorRepo.GetAllActors());
            return Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutActor(int id, [FromForm] ActorDTO actor)
        {
            if (await _actorRepo.UpdateActorAsync(id, actor))
            {
                return Ok("Cap nhat thanh cong");
            }
            else return BadRequest();
               
        }

        // POST: api/Actors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Actor>> PostActor( [FromForm] ActorDTO actor)
        {
            if (await _actorRepo.CreateActorAsync(actor))
            {
                return Ok("Thêm thành công");
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/Actors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActor(int id)
        {
            if (_actorRepo.DeleteActor(id))
            {
                return Ok("Xóa thành công");
            }
            else return BadRequest();
        }

        [HttpGet("search/{keyword}")]
        public async Task<IActionResult> Search(string keyword)
        {
            var data = _actorRepo.SearchActor(keyword);
            return Ok(data);
           
        }

    }
}
