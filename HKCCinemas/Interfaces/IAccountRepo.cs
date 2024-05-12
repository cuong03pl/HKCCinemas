using HKCCinemas.Models;
using Microsoft.AspNetCore.Identity;

namespace HKCCinemas.Interfaces
{
    public interface IAccountRepo
    {
        public Task<string> Login(LoginModel loginModel);
        public  Task<IdentityResult> Register(RegisterModel registerModel);
    }
}
