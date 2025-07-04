﻿using AutoMapper;
using HKCCinemas.DTO;
using HKCCinemas.Helper;
using HKCCinemas.Interfaces;
using HKCCinemas.Models;
using Microsoft.EntityFrameworkCore;

namespace HKCCinemas.Repo
{
    public class TicketRepo : ITicketRepo
    {
        private readonly CinemasContext _context;
        private readonly IMapper _mapper;

        public TicketRepo(CinemasContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int Count()
        {
            return _context.Tickets.Count();
        }
        public bool CreateTicket(TicketDTO ticket)
        {
            var ticketMapper = _mapper.Map<Ticket>(ticket);
            _context.Tickets.Add(ticketMapper);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteTicket(int ticketId)
        {
            var ticket = GetTicketById(ticketId);
            _context.Tickets.Remove(ticket);
            _context.SaveChanges();
            return true;
        }

        public Ticket GetTicketById(int ticketId)
        {
            return _context.Tickets.Where(t => t.Id == ticketId).FirstOrDefault();
        }

        public TicketDTO GetTicketByScheduleId(int scheduleId)
        {
            var ticketMapper = _mapper.Map<TicketDTO>(_context.Tickets.Where(t => t.ScheduleId == scheduleId).FirstOrDefault());
            return ticketMapper;
        }

        public List<TicketViewDTO> GetTickets()
        {
            return _context.Tickets.
                Include(t => t.Schedule).ThenInclude(s => s.Cinemas).
                Include(t => t.Schedule).ThenInclude(t =>t.Film).
                Include(t => t.Schedule).ThenInclude(t => t.ShowDate).Select(t => new TicketViewDTO
                {
                    Id = t.Id,
                    Price = t.Price,
                    StartTime = t.Schedule.StartTime,
                    EndTime = t.Schedule.EndTime,
                    ShowDate = t.Schedule.ShowDate.Date,
                    FilmName = t.Schedule.Film.Title,
                    CinemasName = t.Schedule.Cinemas.Name,
                    ScheduleId = t.Schedule.Id,
                    Count = _context.Tickets.Count()
                    
                }).ToList();
        }

        public List<TicketViewDTO> Search(QueryObject query)
        {
            var tickets = _context.Tickets.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                tickets = tickets.Where(c => c.Schedule.Film.Title.Contains(query.Keyword));
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return tickets.
                Include(t => t.Schedule).ThenInclude(s => s.Cinemas).
                Include(t => t.Schedule).ThenInclude(t => t.Film).
                Include(t => t.Schedule).ThenInclude(t => t.ShowDate).Select(t => new TicketViewDTO
                {
                    Id = t.Id,
                    Price = t.Price,
                    StartTime = t.Schedule.StartTime,
                    EndTime = t.Schedule.EndTime,
                    ShowDate = t.Schedule.ShowDate.Date,
                    FilmName = t.Schedule.Film.Title,
                    CinemasName = t.Schedule.Cinemas.Name,
                    ScheduleId = t.Schedule.Id,
                    Count = tickets.Count(),
                }).Skip(skipNumber).Take(query.PageSize).ToList();
        }

        public bool UpdateTicket(int ticketId, TicketDTO ticket)
        {
            var ticketNow = GetTicketById(ticketId);
            ticketNow.Price = ticket.Price;
            ticketNow.ScheduleId = ticket.ScheduleId;
            _context.Tickets.Update(ticketNow);
            _context.SaveChanges();
            return true;
        }
    }
}
