using HKCCinemas.Models;

namespace HKCCinemas.Interfaces
{
    public interface IUserRepo
    {
        List<User> GetAllUsers();
        User GetUserById(string id);
        User GetUserByUserName(string username);
         Task<bool> DeleteUser(string id);
        int GetCountUser();
        List<User> Search(string keyword);

    }
}
