﻿using AutoMapper;
using HKCCinemas.DTO;
using HKCCinemas.Helper;
using HKCCinemas.Interfaces;
using HKCCinemas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HKCCinemas.Repo
{
    public class SeatRepo : ISeatRepo
    {
        private readonly CinemasContext _context;
        private readonly IMapper _mapper;
        public SeatRepo(CinemasContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Count()
        {
            return _context.Seats.Count();
        }
        public bool CreateSeat(SeatDTO seat)
        {
            var seatMapper = _mapper.Map<Seat>(seat);
            _context.Seats.Add(seatMapper);
            _context.SaveChanges();
            return true;

        }

        public bool DeleteSeat(int seatId)
        {
            var seat = _context.Seats.Where(s => s.Id == seatId).FirstOrDefault();
            _context.Seats.Remove(seat);
            _context.SaveChanges();
            return true;
        }

        public List<SeatViewDTO> GetAllSeats()
        {
            return _context.Seats.Include(s => s.Room).ThenInclude(r => r.Cinemas).Select(s => new SeatViewDTO
            {
                Id = s.Id,
                Name = s.Name,
                Cinemas= _mapper.Map<CinemasDTO>(s.Room.Cinemas),
                Room= _mapper.Map<RoomDTO>(s.Room),
                Count= _context.Seats.Count()
            }).ToList();
        }

        public SeatViewDTO GetSeatById(int seatId)
        {
            return _context.Seats.Where(s => s.Id == seatId)
                .Include(s => s.Room).ThenInclude(r => r.Cinemas).Select(s => new SeatViewDTO
            {
                Id = s.Id,
                Name = s.Name,
                Cinemas= _mapper.Map<CinemasDTO>(s.Room.Cinemas),
                Room= _mapper.Map<RoomDTO>(s.Room),
            }).FirstOrDefault();
        }

        public List<SeatViewDTO> GetSeatByRoomId(int roomId)
        {
            return _context.Seats.Where(s => s.RoomID == roomId).Include(s => s.Room).ThenInclude(r => r.Cinemas).Select(s => new SeatViewDTO
            {
                Id = s.Id,
                Name = s.Name,
                Cinemas = _mapper.Map<CinemasDTO>(s.Room.Cinemas),
                Room = _mapper.Map<RoomDTO>(s.Room),
            }).ToList();
        }

        public List<SeatViewDTO> GetSeatsByIds(int[] seatIds)
        {
            List<SeatViewDTO> list = new List<SeatViewDTO>();
            foreach (var id in seatIds)
            {
              var seat =  _context.Seats.Where(s => s.Id == id).Include(s => s.Room).ThenInclude(r => r.Cinemas).Select(s => new SeatViewDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Cinemas = _mapper.Map<CinemasDTO>(s.Room.Cinemas),
                    Room = _mapper.Map<RoomDTO>(s.Room),
                }).FirstOrDefault();
                list.Add(seat);
            }

            return list;
        }

        public bool isAvailable(int seatId, int scheduleId)
        {
            var seat = _context.SeatStatuses.Where(s => s.SeatId == seatId && s.ScheduleId == scheduleId);
            return seat.Any();
        }

        public List<SeatViewDTO> Search(QueryObject query)
        {

            var seats = _context.Seats.Include(s => s.Room).ThenInclude(r => r.Cinemas).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                seats = seats.Where(c => c.Room.Cinemas.Name.Contains(query.Keyword));
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return seats.Select(s => new SeatViewDTO
            {
                Id = s.Id,
                Name = s.Name,
                Cinemas = _mapper.Map<CinemasDTO>(s.Room.Cinemas),
                Room = _mapper.Map<RoomDTO>(s.Room),
                Count = seats.Count(),
            }).Skip(skipNumber).Take(query.PageSize).ToList();
        }

        public bool UpdateSeat(int seatId, SeatDTO seat)
        {
            var seatNow = _context.Seats.Where(s => s.Id == seatId).FirstOrDefault();
            seatNow.Name = seat.Name;
            _context.Seats.Update(seatNow);
            _context.SaveChanges();
            return true;
        }

        
    }
}
