using HKCCinemas.DTO;
using HKCCinemas.Helper;
using HKCCinemas.Models;

namespace HKCCinemas.Interfaces
{
    public interface ISeatRepo
    {
        List<SeatViewDTO> GetAllSeats();
        SeatViewDTO GetSeatById(int seatId);
        List<SeatViewDTO> GetSeatByRoomId(int roomId);
        List<SeatViewDTO> GetSeatsByIds(int[] seatIds);
        bool CreateSeat(SeatDTO seat);
        bool UpdateSeat(int seatId, SeatDTO seat);
        bool DeleteSeat(int seatId);
        bool isAvailable (int seatId, int scheduleId);
        List<SeatViewDTO> Search(QueryObject query);
        int Count();


    }
}
