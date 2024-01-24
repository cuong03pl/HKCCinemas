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
            var data = _mapper.Map<List<ActorDTO>> (_actorRepo.GetAllActors(film_id));
            return Ok(data);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutActor(int id, [FromBody] ActorDTO actor)
        {
            if (id != actor.Id)
            {
                return BadRequest();
            }
            try
            {
                var actorMapper = _mapper.Map<Actor>(actor);
                _actorRepo.UpdateActor(actorMapper);
                return Ok("Sửa thành công");
            }
            catch (DbUpdateConcurrencyException)
            {
               
            }

            return NoContent();
        }

        // POST: api/Actors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{filmId}")]
        public async Task<ActionResult<Actor>> PostActor( int filmId,[FromBody] ActorDTO actor)
        {
            var actorMapper = _mapper.Map<ActorDTO, Actor>(actor);
            if (_actorRepo.CreateActor(filmId, actorMapper))
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

       
    }
}
