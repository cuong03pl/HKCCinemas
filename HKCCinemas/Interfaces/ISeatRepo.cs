using HKCCinemas.DTO;
using HKCCinemas.Models;

namespace HKCCinemas.Interfaces
{
    public interface ISeatRepo
    {
        List<SeatViewDTO> GetAllSeats();
        SeatViewDTO GetSeatById(int seatId);
        List<SeatViewDTO> GetSeatByRoomId(int roomId, int cinemasId);
        List<SeatViewDTO> GetSeatsByIds(int[] seatIds);
        bool CreateSeat(SeatDTO seat);
        bool UpdateSeat(int seatId, SeatDTO seat);
        bool DeleteSeat(int seatId);
        bool isAvailable (int seatId, int scheduleId);
        List<SeatViewDTO> Search(string keyword);

    }
}
