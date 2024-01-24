using HKCCinemas.Models;

namespace HKCCinemas.Interfaces
{
    public interface IUserRepo
    {
        List<User> GetAllUsers();
        User GetUserById(int id);
        User GetUserByUserName(string username);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(int id);
        int GetCountUser();

    }
}
