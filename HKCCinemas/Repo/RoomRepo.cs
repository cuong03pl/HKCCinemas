using AutoMapper;
using HKCCinemas.DTO;
using HKCCinemas.Helper;
using HKCCinemas.Interfaces;
using HKCCinemas.Models;
using Microsoft.EntityFrameworkCore;

namespace HKCCinemas.Repo
{
    public class RoomRepo : IRoomRepo
    {
        private readonly CinemasContext _context;
        private readonly IMapper _mapper;
        public RoomRepo(CinemasContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Count()
        {
            return _context.Rooms.Count();
        }

        public bool createRoom(RoomDTO room)
        {
            var roomMapper = _mapper.Map<Room>(room);
            _context.Rooms.Add(roomMapper);
            _context.SaveChanges();
            return true;
        }

        public bool deleteRoom(int id)
        {
            var room = GetRoomById(id);
            _context.Rooms.Remove(room);
            _context.SaveChanges();
            return true;
        }

        public Room GetRoomById(int id)
        {
           var room = _context.Rooms.Where(r => r.Id == id).FirstOrDefault();
            return room;
        }
        public List<Room> GetRoomByCinemasId(int cinemasId)
        {
            var room = _context.Rooms.Where(r => r.CinemasId == cinemasId).ToList();
            return room;
        }
        public List<RoomViewDTO> GetRooms()
        {
            return _context.Rooms.Include(r => r.Cinemas).Select(r => new RoomViewDTO
            {
                Id = r.Id,
                Cinemas = _mapper.Map<CinemasDTO>(r.Cinemas),
                RoomName = r.RoomName,
                Count = _context.Rooms.Count()
            }).ToList();
        }

        public bool updateRoom(int id ,RoomDTO room)
        {
           var roomNow = GetRoomById(id);
            roomNow.RoomName = room.RoomName;
            var roomMapper = _mapper.Map<Room>(roomNow);
            _context.Rooms.Update(roomMapper);
            _context.SaveChanges();
            return true;
        }

        public bool isCinemaRoomOccupied(int cinemasId, int filmId, int roomId, int showDateId, TimeSpan startTime)
        {
            var film = _context.Film.Where(f => f.Id == filmId).FirstOrDefault();


            var endTime = startTime.Add(TimeSpan.FromMinutes(film.Duration));
            var endTimeFilm = new TimeSpan((int)(endTime.TotalHours > 24 ? endTime.TotalHours % 24 : endTime.TotalHours), endTime.Minutes,0);

            var result = _context.Schedules.
                Where(s => s.RoomId == roomId && s.CinemasId == cinemasId
                && s.ShowDateId == showDateId ).ToList();
            int temp = 0;
            foreach (var item in result)
            {
                if (item.EndTime.Hours <= 5 && item.EndTime.Hours >= 0)
                {
                    var endTimeConvert = new TimeSpan(item.EndTime.Hours + 24, item.EndTime.Minutes, 0);
                    if (startTime >= item.StartTime && startTime.Hours <= endTimeConvert.Hours)
                    {
                        temp++;
                    }
                    else if (endTime.Hours >= item.StartTime.Hours && endTime.Hours <= endTimeConvert.Hours)
                    {
                        temp++;
                    }
                    else if (startTime <= item.StartTime && endTime.Hours >= item.EndTime.Hours) temp++; 
                    else if(startTime >= item.StartTime && endTime.Hours<= item.EndTime.Hours) temp++; 
                }
                else
                {
                    if(startTime >= item.StartTime && startTime <= item.EndTime)
                    {
                        temp++;
                    }
                    else if(endTimeFilm >= item.StartTime &&  endTimeFilm <= item.EndTime)
                    {
                        temp++;
                    }
                    else if (startTime <= item.StartTime && endTime >= item.EndTime) temp++;
                    else if (startTime >= item.StartTime && endTime <= item.EndTime) temp++;
                }
            }
            return temp > 0 ? true : false;
        }

        public List<RoomViewDTO> Search(QueryObject query)
        {
            var rooms = _context.Rooms.Include(r => r.Cinemas).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                rooms = rooms.Where(c => c.Cinemas.Name.Contains(query.Keyword));
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
           return rooms.Select(r => new RoomViewDTO
            {
                Id = r.Id,
                Cinemas = _mapper.Map<CinemasDTO>(r.Cinemas),
                RoomName = r.RoomName,
                Count = rooms.Count(),
            }).Skip(skipNumber).Take(query.PageSize).ToList();
        }
    }
}
