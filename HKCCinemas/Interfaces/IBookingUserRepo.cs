using HKCCinemas.DTO;
using HKCCinemas.Models;

namespace HKCCinemas.Interfaces
{
    public interface IBookingUserRepo
    {
        List<BookingUserlViewDTO> GetAllBookingUsers();
        List<BookingUserlViewDTO> GetAllBookingUsersByUserId(string userid);
        Task<bool> CreateBookingUser(BookingUserDTO bookingDetail);
        bool DeleteBookingUser(int id);

        int GetCountTicket();

        List<object> GetTop5Films();

        List<object> GetTotalMoney();
    }
}
