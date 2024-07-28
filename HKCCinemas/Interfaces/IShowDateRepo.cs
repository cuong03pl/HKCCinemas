using HKCCinemas.DTO;
using HKCCinemas.Helper;
using HKCCinemas.Models;

namespace HKCCinemas.Interfaces
{
    public interface IShowDateRepo
    {
        List<ShowDateViewDTO> GetAllShowDate();
        List<ShowDate> GetAllShowDateByCinemasId(int cinemasId);
        ShowDate GetShowDateById(int showdateId);
       
        bool CreateShowDate(ShowDateDTO showdate);
        bool UpdateShowDate(int showdateId, ShowDateDTO showdate);
        bool DeleteShowDate(int showdateId);
        List<ShowDateViewDTO> Search(QueryObject query);
        int Count();


    }
}
