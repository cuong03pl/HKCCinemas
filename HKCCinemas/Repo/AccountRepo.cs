using HKCCinemas.Interfaces;
using HKCCinemas.Models;
using Microsoft.AspNetCore.Identity;
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

        public AccountRepo(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
                IConfiguration configuration, SignInManager<User> signInManager
            ) {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }
        public async Task<string> Login(LoginModel loginModel)
        {
                var result = await signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password, false, false);
                if (result.Succeeded) {
                var authClaims = new List<Claim>
                    {
                    new Claim(ClaimTypes.NameIdentifier, loginModel.Username),
                        new Claim(ClaimTypes.Name, loginModel.Username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };
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

            var newUser = new User()
            {
                UserName = registerModel.Username,
                Email = registerModel.Email,
            };
            return await userManager.CreateAsync(newUser, registerModel.Password );
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
