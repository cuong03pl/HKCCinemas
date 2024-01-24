using HKCCinemas.Models;

namespace HKCCinemas.Interfaces
{
    public interface ICinemasRepo
    {
        List<Cinemas> GetAllCinemas();
        Cinemas GetCinemasById(int id);
        bool CreateCinemas(Cinemas cinemas);
        bool UpdateCinemas(Cinemas cinemas);
        bool DeleteCinemas(int id);
        int CountCinemas();
    }
}
