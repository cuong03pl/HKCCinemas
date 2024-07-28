using HKCCinemas.Models;

namespace HKCCinemas.DTO
{
    public class UserViewDTO: User
    {
        public int Count { get; set; } = 0;
    }
}
