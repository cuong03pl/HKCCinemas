using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HKCCinemas.Models;
using HKCCinemas.Interfaces;
using HKCCinemas.Repo;
using HKCCinemas.DTO;
using HKCCinemas.Helper;

namespace HKCCinemas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly CinemasContext _context;
        private readonly ITicketRepo _ticketRepo;
        private readonly IScheduleRepo _scheduleRepo;

        public TicketsController(CinemasContext context, ITicketRepo ticketRepo, IScheduleRepo scheduleRepo)
        {
            _context = context;
            _ticketRepo = ticketRepo;
            _scheduleRepo = scheduleRepo;
        }

        [HttpGet("getCount")]
        public async Task<ActionResult<int>> Count()
        {
            var data = _ticketRepo.Count();
            return Ok(data);
        }

        // GET: api/Tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketViewDTO>>> GetTickets()
        {
          var data = _ticketRepo.GetTickets();
            
            return Ok(data);
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
         var data = _ticketRepo.GetTicketById(id);
            return Ok(data);
        }

        [HttpGet("GetTicketByScheduleId/{scheduleId}")]
        public async Task<ActionResult<TicketDTO>> GetTicketByScheduleId(int scheduleId)
        {
            var data = _ticketRepo.GetTicketByScheduleId(scheduleId);
            return Ok(data);
        }



        // PUT: api/Tickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(int id, [FromForm] TicketDTO ticket)
        {
            if (_ticketRepo.UpdateTicket(id, ticket))
            {
                return Ok("Sửa thành công");
            }
            else return BadRequest("Sửa thất bại");
        }

        // POST: api/Tickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket([FromForm] TicketDTO ticket)
        {
            if (_ticketRepo.CreateTicket(ticket))
            {
                return Ok("Tạo thành công");
            }
            else return BadRequest("Tạo thất bại");
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            if (_ticketRepo.DeleteTicket(id))
            {
                return Ok("Xóa thành công");
            }
            else return BadRequest("Xóa thất bại");
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] QueryObject query)
        {
            var data = _ticketRepo.Search(query);

            return Ok(data);

        }

    }
}
