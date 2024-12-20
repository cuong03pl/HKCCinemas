using AutoMapper;
using HKCCinemas.DTO;
using HKCCinemas.Helper;
using HKCCinemas.Interfaces;
using HKCCinemas.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HKCCinemas.Repo
{
    public class ShowDateRepo : IShowDateRepo
    {
        private readonly CinemasContext _context;
        private readonly IMapper _mapper;
        public ShowDateRepo(CinemasContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int Count()
        {
            return _context.ShowDates.Count();
        }
        public bool CreateShowDate(ShowDateDTO showdate)
        {
            var showdateMapper = _mapper.Map<ShowDate>(showdate);
            _context.ShowDates.Add(showdateMapper);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteShowDate(int showdateId)
        {
           var showdate = GetShowDateById(showdateId);
            _context.ShowDates.Remove(showdate);
            _context.SaveChanges();
            return true;
        }

        public List<ShowDateViewDTO> Search(QueryObject query)
        {
            var showDates = _context.ShowDates.Include(s => s.Cinemas).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                showDates = showDates.Where(a => a.Cinemas.Name.Contains(query.Keyword));
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return showDates.Select(s => new ShowDateViewDTO
            {
                Id = s.Id,
                Date = s.Date,
                Cinemas = _mapper.Map<CinemasDTO>(s.Cinemas),
                Count = showDates.Count(),
            }).Skip(skipNumber).Take(query.PageSize).ToList();
        }

        public List<ShowDate> GetAllShowDateByCinemasId(int cinemasId)
        {
            return _context.ShowDates.Where(sd => sd.CinemasId == cinemasId).OrderBy(s => s.Date).ToList();
        }

        

        public ShowDate GetShowDateById(int showdateId)
        {
            return _context.ShowDates.Where(sd => sd.Id == showdateId).FirstOrDefault();

        }
        
        public List<ShowDateViewDTO> GetAllShowDate()
        {
            return _context.ShowDates.Include(s => s.Cinemas).Select(s => new ShowDateViewDTO
            {
                Id = s.Id,
                Date = s.Date,
                Cinemas = _mapper.Map<CinemasDTO>(s.Cinemas),
                Count = _context.ShowDates.Count(),
            }).Skip(0).Take(5).ToList();
        }

        public bool UpdateShowDate(int showdateId, ShowDateDTO showdate)
        {
            var showdateNow = GetShowDateById(showdateId);
            showdateNow.Date = showdate.Date;
            showdateNow.CinemasId = showdate.CinemasId;
            _context.ShowDates.Update(showdateNow);
            _context.SaveChanges();
            return true;
        }
    }
}
