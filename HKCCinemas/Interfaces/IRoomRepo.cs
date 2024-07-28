using HKCCinemas.DTO;
using HKCCinemas.Helper;
using HKCCinemas.Models;

namespace HKCCinemas.Interfaces
{
    public interface IRoomRepo
    {
        Room GetRoomById(int id);
        List<Room> GetRoomByCinemasId(int cinemasId);
        List<RoomViewDTO> GetRooms();
        bool createRoom(RoomDTO room);
        bool updateRoom(int id, RoomDTO room);
        bool deleteRoom(int id);
        int Count();
        bool isCinemaRoomOccupied(int cinemasId, int filmId, int roomId, int showDateId, TimeSpan startTime);
        List<RoomViewDTO> Search(QueryObject query);

    }
}
