using HKCCinemas.DTO;
using HKCCinemas.Models;

namespace HKCCinemas.Interfaces
{
    public interface IShowDateRepo
    {
        List<ShowDate> GetAllShowDate();
        List<ShowDate> GetAllShowDateByCinemasId(int cinemasId);
        ShowDate GetShowDateById(int showdateId);
       
        bool CreateShowDate(ShowDateDTO showdate);
        bool UpdateShowDate(int showdateId, ShowDateDTO showdate);
        bool DeleteShowDate(int showdateId);
    }
}
