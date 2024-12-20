using AutoMapper;
using HKCCinemas.DTO;
using HKCCinemas.Helper;
using HKCCinemas.Interfaces;
using HKCCinemas.Models;
using Microsoft.EntityFrameworkCore;

namespace HKCCinemas.Repo
{
    public class BookingUserRepo : IBookingUserRepo
    {
        private readonly CinemasContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _evn;

        public BookingUserRepo(CinemasContext context, IMapper mapper, IWebHostEnvironment evn)
        {
            _context = context;
            _mapper = mapper;
            _evn = evn;
        }

        public async Task<bool> CreateBookingUser(BookingUserDTO bookingUser)
        {
            
            var bookingUserAdd = new BookingUser
            {
                BookingDetails = new List<BookingDetail>(),
                OrderCode = bookingUser.OrderCode,
                UserId = bookingUser.UserId,
                BookingDate = DateTime.Now,
            };
            foreach (var item in bookingUser.SeatIds)
            {

                var bookingDetail = new BookingDetail
                {
                    SeatId = item,
                    TicketId = bookingUser.TicketId,
                };
                bookingUserAdd.BookingDetails.Add(bookingDetail);

                var ticket = _context.Tickets.Where(t => t.Id == bookingUser.TicketId).FirstOrDefault();

                var seat = _context.Seats.Where(s => s.Id == item).FirstOrDefault();

                var seatStatus = _context.SeatStatuses.Where(s => s.SeatId == item && s.ScheduleId == ticket.ScheduleId);

                if (seatStatus.Any())
                {
                    return false;
                }
                var seatStatusAdd = new SeatStatus { ScheduleId =  ticket.ScheduleId, SeatId = item};
                _context.SeatStatuses.Add(seatStatusAdd);
               
            }
                _context.BookingUsers.Add(bookingUserAdd);
                
                await _context.SaveChangesAsync();
            return true;
        }

        public bool DeleteBookingUser(int id)
        {
            throw new NotImplementedException();
        }

        public List<BookingUserlViewDTO> GetAllBookingUsers()
        {
          var data =  _context.BookingUsers.Include(bu => bu.BookingDetails).Select(bu => new BookingUserlViewDTO {
              Id = bu.Id,
              UserId = bu.UserId,
              OrderCode = bu.OrderCode,
              Detail = bu.BookingDetails.Select(bd => new BookingDetailViewDTO
              {
                  Id = bd.Id,
                  Seat = _mapper.Map<SeatDTO>(bd.Seat),
                  Ticket = _mapper.Map<TicketDTO>(bd.Ticket),
                  Schedule = new ScheduleViewDTO
                  {
                      Film = _mapper.Map<FilmDTO>(bd.Ticket.Schedule.Film),
                      Room = _mapper.Map<RoomDTO>(bd.Ticket.Schedule.Room),
                      ShowDate = _mapper.Map<ShowDateDTO>(bd.Ticket.Schedule.ShowDate),
                      Cinemas = _mapper.Map<CinemasDTO>(bd.Ticket.Schedule.Cinemas),
                      StartTime = bd.Ticket.Schedule.StartTime,
                      EndTime = bd.Ticket.Schedule.EndTime,
                  }

              }).ToList()

          } ).ToList();
            return data;
        }

        public List<BookingUserlViewDTO> GetAllBookingUsersByUserId(string userid)
        {
            var data = _context.BookingUsers.Where(bu => bu.UserId == userid).Include(bu => bu.BookingDetails).Select(bu => new BookingUserlViewDTO
            {
                Id = bu.Id,
                UserId = bu.UserId,
                OrderCode = bu.OrderCode,
                Detail = bu.BookingDetails.Select(bd => new BookingDetailViewDTO
                {
                    Id = bd.Id,
                    Seat = _mapper.Map<SeatDTO>(bd.Seat),
                    Ticket = _mapper.Map<TicketDTO>(bd.Ticket),
                    Schedule = new ScheduleViewDTO
                    {
                        Film = _mapper.Map<FilmDTO>(bd.Ticket.Schedule.Film),
                        Room = _mapper.Map<RoomDTO>(bd.Ticket.Schedule.Room),
                        ShowDate = _mapper.Map<ShowDateDTO>(bd.Ticket.Schedule.ShowDate),
                        Cinemas = _mapper.Map<CinemasDTO>(bd.Ticket.Schedule.Cinemas),
                        StartTime = bd.Ticket.Schedule.StartTime,
                        EndTime = bd.Ticket.Schedule.EndTime,
                    }

                }).ToList()

            }).ToList();
            return data;
        }

        public int GetCountTicket()
        {
            return _context.BookingUsers.Count();
        }
        public List<object> GetTop5Films()
        {
            var bookingDetails = _context.BookingDetails
                  .Include(bd => bd.Ticket)
                  .ThenInclude(t => t.Schedule)
                  .ThenInclude(s => s.Film)
                  .AsEnumerable()
                  .GroupBy(bd => new
                  {
                      Film = bd.Ticket.Schedule.Film
                  })
                  .Select(g => new
                  {
                      Id = g.Key.Film.Id,
                      Title = g.Key.Film.Title,
                      Detail = g.Key.Film.Detail,
                      Synopsis = g.Key.Film.Synopsis,
                      AgeLimit = g.Key.Film.AgeLimit,
                      Duration = g.Key.Film.Duration,
                      Country = g.Key.Film.Country,
                      Rating = g.Key.Film.Rating,
                      ReleaseDate = g.Key.Film.ReleaseDate,
                      EndDate = g.Key.Film.EndDate,
                      Director = g.Key.Film.Director,
                      Image = g.Key.Film.Image,
                      Background = g.Key.Film.Background,
                      SoldCount = g.Count()
                  })
                  .OrderByDescending(x => x.SoldCount)
                  .Take(5)
                  .ToList<object>();
            return bookingDetails;
        }


        public List<BookingDetailDTO> GetAllBookingDetails(QueryObject query)
        {
            var bookingDetails = _context.BookingDetails.
                 Include(bd => bd.Ticket)
                 .Include(bd => bd.Seat)
                 .Include(bd => bd.BookingUser).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                bookingDetails = bookingDetails.Where(bd => bd.BookingUser.User.UserName.Contains(query.Keyword));
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            var data = bookingDetails
                 .Select(bd => new BookingDetailDTO
                 {
                     Id = bd.Id,
                     Schedule = new ScheduleViewDTO
                     {
                         Id = bd.Ticket.Id,
                         Cinemas = _mapper.Map<CinemasDTO>(bd.Ticket.Schedule.Cinemas),
                         Film = _mapper.Map<FilmDTO>(bd.Ticket.Schedule.Film),
                         Room = _mapper.Map<RoomDTO>(bd.Ticket.Schedule.Room),
                         ShowDate = _mapper.Map<ShowDateDTO>(bd.Ticket.Schedule.ShowDate),
                         EndTime = bd.Ticket.Schedule.EndTime,
                         StartTime = bd.Ticket.Schedule.StartTime,
                     },
                     Ticket = _mapper.Map<TicketDTO>(bd.Ticket),
                     Seat = _mapper.Map<SeatDTO>(bd.Seat),
                     User = bd.BookingUser.User,
                     Count = bookingDetails.Count()
                 }).Skip(skipNumber).Take(query.PageSize).ToList();
            return data;
        }

        public List<object> GetTotalMoney()
        {
            var totalByMonth = new List<object>();
            for (int month = 1; month <= 12; month++)
            {
                var result = _context.BookingDetails.Include(bd => bd.Ticket).Include(bd => bd.BookingUser)
                    .Where(x => x.BookingUser.BookingDate.HasValue && x.BookingUser.BookingDate.Value.Month == month
                )
                .GroupBy(x => new {
                    Price = x.Ticket.Price,
                })
                .Select(x => new
                {
                    Total = x.Key.Price * x.Count(),
                    Month = month
                }).FirstOrDefault();

                if (result == null)
                {
                    totalByMonth.Add(new { Month = month, Total = 0 });
                }
                else totalByMonth.Add(result);
            }
            

            return totalByMonth;
        }
    }
}
