using AutoMapper;
using HKCCinemas.DTO;
using HKCCinemas.Interfaces;
using HKCCinemas.Models;

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

        public List<ShowDate> GetAllShowDate()
        {
           return _context.ShowDates.ToList();
        }

        public List<ShowDate> GetAllShowDateByCinemasId(int cinemasId)
        {
            return _context.ShowDates.Where(sd => sd.CinemasId == cinemasId).OrderBy(s => s.Date).ToList();
        }

        public ShowDate GetShowDateById(int showdateId)
        {
            return _context.ShowDates.Where(sd => sd.Id == showdateId).FirstOrDefault();

        }

        public List<ShowDate> Search(string keyword)
        {
            return _context.ShowDates.Where(s => s.Cinemas.Name.Contains(keyword)).ToList();
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
