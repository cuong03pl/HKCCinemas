using AutoMapper;
using HKCCinemas.DTO;
using HKCCinemas.Interfaces;
using HKCCinemas.Models;
using Microsoft.EntityFrameworkCore;

namespace HKCCinemas.Repo
{
    public class ScheduleRepo : IScheduleRepo
    {
        private readonly CinemasContext _context;
        private readonly IMapper _mapper;
        public ScheduleRepo(CinemasContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool CreateSchedule(ScheduleDTO schedule)
        {
            var film = _context.Film.Where(f => f.Id == schedule.FilmId).FirstOrDefault();
            var scheduleMapper = _mapper.Map<Schedule>(schedule);
            var endTime = scheduleMapper.StartTime.Add(TimeSpan.FromMinutes(film.Duration));
          
                scheduleMapper.EndTime = new TimeSpan((int)(endTime.TotalHours > 24 ? endTime.TotalHours % 24 : endTime.TotalHours), endTime.Minutes, 0);

            _context.Schedules.Add(scheduleMapper);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteSchedule(int scheduleId)
        {
            var schedule = _context.Schedules.Where(s => s.Id == scheduleId).FirstOrDefault();
            _context.Schedules.Remove(schedule);
            _context.SaveChanges(true);
            return true;
        }

        public List<ScheduleViewDTO> GetAllSchedule()
        {
           return _context.Schedules.Include(s => s.Film).Include(s => s.Cinemas).Include(s => s.Room).Select(s => new ScheduleViewDTO
           {
               Id= s.Id,
               Cinemas = _mapper.Map<CinemasDTO>(s.Cinemas),
               Film = _mapper.Map<FilmDTO>(s.Film),
               Room = _mapper.Map<RoomDTO>(s.Room),
               ShowDate = _mapper.Map<ShowDateDTO>(s.ShowDate),
               StartTime = s.StartTime, EndTime = s.EndTime,
           }).ToList();
        }

        public List<ScheduleViewDTO> GetAllScheduleByFilmIdAndRoomId(int filmId, int roomId)
        {
            var data = _context.Schedules.Where(s => s.FilmId == filmId && s.RoomId == roomId)
                .Select(s => new ScheduleViewDTO
            {
                Id = s.Id,
                Cinemas = _mapper.Map<CinemasDTO>(s.Cinemas),
                Film = _mapper.Map<FilmDTO>(s.Film),
                Room = _mapper.Map<RoomDTO>(s.Room),
                ShowDate = _mapper.Map<ShowDateDTO>(s.ShowDate),
                StartTime = s.StartTime,
                EndTime = s.EndTime,
            }).ToList();
            return data;
        }
        public List<ScheduleViewDTO> GetAllScheduleByShowDateAndCinemas(int showDateId, int cinemasId)
        {
            var data = _context.Schedules.Where(s => s.ShowDateId == showDateId && s.CinemasId == cinemasId).Include(s => s.Film).Include(s => s.Cinemas).Select(s => new ScheduleViewDTO
            {
                Id = s.Id,
                Cinemas = _mapper.Map<CinemasDTO>(s.Cinemas),
                Film = _mapper.Map<FilmDTO>(s.Film),
                Room = _mapper.Map<RoomDTO>(s.Room),
                ShowDate = _mapper.Map<ShowDateDTO>(s.ShowDate),
                StartTime = s.StartTime,
                EndTime = s.EndTime,
            }).ToList();
            return data;
        }
        public ScheduleDTO GetScheduleByShowDateAndCinemasAndFilm(int showDateId, int cinemasId, int filmId)
        {
            var data = _mapper.Map<ScheduleDTO>(_context.Schedules.
                Where(s => s.ShowDateId == showDateId && s.CinemasId == cinemasId && s.FilmId == filmId).FirstOrDefault()) ;

            return data;
        }
        public ScheduleViewDTO GetScheduleById(int scheduleId)
        {
            return _context.Schedules.Where(s => s.Id == scheduleId).Include(s => s.Film).Include(s => s.Cinemas).Select(s => new ScheduleViewDTO
            {
                Id = s.Id,
                Cinemas = _mapper.Map<CinemasDTO>(s.Cinemas),
                Film = _mapper.Map<FilmDTO>(s.Film),
                Room = _mapper.Map<RoomDTO>(s.Room),
                ShowDate = _mapper.Map<ShowDateDTO>(s.ShowDate),
                StartTime = s.StartTime,
                EndTime = s.EndTime,
            }).FirstOrDefault();
        }

        public bool UpdateSchedule(int scheduleId, ScheduleDTO schedule)
        {
            var scheduleNow = _context.Schedules.Where(s => s.Id == scheduleId).FirstOrDefault();
            var film = _context.Film.Where(f => f.Id == schedule.FilmId).FirstOrDefault();
            scheduleNow.ShowDateId = schedule.ShowDateId;
            scheduleNow.FilmId = schedule.FilmId;
            scheduleNow.RoomId = schedule.RoomId;
            scheduleNow.CinemasId = schedule.CinemasId;
            scheduleNow.StartTime = schedule.StartTime;
            scheduleNow.EndTime = schedule.StartTime.Add(TimeSpan.FromMinutes(film.Duration)); ;
            _context.Schedules.Update(scheduleNow);
            _context.SaveChanges();
            return true;
        }

        public List<ScheduleViewDTO> Search(string keyword)
        {
            var data = _context.Schedules.Include(s => s.Film).Include(s => s.Cinemas).Where(s => s.Film.Title.Contains(keyword)).Select(s => new ScheduleViewDTO
            {
                Id = s.Id,
                Cinemas = _mapper.Map<CinemasDTO>(s.Cinemas),
                Film = _mapper.Map<FilmDTO>(s.Film),
                Room = _mapper.Map<RoomDTO>(s.Room),
                ShowDate = _mapper.Map<ShowDateDTO>(s.ShowDate),
                StartTime = s.StartTime,
                EndTime = s.EndTime,
            }).ToList();
            return data;
        }
    }
}
