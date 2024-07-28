using HKCCinemas.DTO;
using HKCCinemas.Helper;
using HKCCinemas.Models;

namespace HKCCinemas.Interfaces
{
    public interface IUserRepo
    {
        List<UserViewDTO> GetAllUsers();
        User GetUserById(string id);
        User GetUserByUserName(string username);
         Task<bool> DeleteUser(string id);
        int Count();
        List<UserViewDTO> Search(QueryObject query);

    }
}
