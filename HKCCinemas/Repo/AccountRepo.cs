using HKCCinemas.Helper;
using HKCCinemas.Interfaces;
using HKCCinemas.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HKCCinemas.Repo
{
    public class AccountRepo : IAccountRepo
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<User> signInManager;
        private readonly RandomAvatar randomAvatar;

        public AccountRepo(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
                IConfiguration configuration, SignInManager<User> signInManager, RandomAvatar randomAvatar
            ) {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.randomAvatar = randomAvatar;
        }
        public async Task<string> Login(LoginModel loginModel)
        {
                var result = await signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password, false, false);

            if (result.Succeeded) {
                var user = await userManager.FindByNameAsync(loginModel.Username);
                var userRoles = await userManager.GetRolesAsync(user);
                var userId = user.Id;
                var authClaims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Name, userId),
                        new Claim(ClaimTypes.Name, loginModel.Username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }
                var token = GetToken(authClaims);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            return null;
                    
        }

        public async Task<IdentityResult> Register(RegisterModel registerModel)
        {
            if (registerModel.Password != registerModel.ConfirmPassword)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Mật khẩu và mật khẩu xác nhận không khớp." });
            }
            var existingUserEmail = userManager.Users
    .FirstOrDefault(u => u.Email == registerModel.Email);
            if (existingUserEmail != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Email đã tồn tại." });
            }

            var existingUserName = userManager.Users
    .FirstOrDefault(u => u.UserName == registerModel.Username);
            if (existingUserName != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Username đã tồn tại." });
            }
            var newUser = new User()
            {
                UserName = registerModel.Username,
                Email = registerModel.Email,
                Avatar = randomAvatar.GenerateRandomAvatar(),
            };
            var result = await userManager.CreateAsync(newUser, registerModel.Password);

            if (!await roleManager.RoleExistsAsync(AppRole.Customer))
            {
                await roleManager.CreateAsync(new IdentityRole(AppRole.Customer));

            }
            await userManager.AddToRoleAsync(newUser, AppRole.Customer);
            return result;
                
        }



        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
               
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
