using HKCCinemas.Helper;
using HKCCinemas.Interfaces;
using HKCCinemas.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HKCCinemas.Repo
{
    public class RoleRepo : IRoleRepo
    {

        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<User> signInManager;
        private readonly RandomAvatar randomAvatar;

        public RoleRepo(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
                IConfiguration configuration, SignInManager<User> signInManager, RandomAvatar randomAvatar
            )
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.randomAvatar = randomAvatar;
        }
        public int Count()
        {
            return roleManager.Roles.Count();
        }
        public async Task<bool> CreateRole(string roleName)
        {
            if(await roleManager.RoleExistsAsync(roleName))
            {
                    return false;
            }
            else
            {
              var result = await  roleManager.CreateAsync(new IdentityRole { Name = roleName });
                if (result.Succeeded)
                {
                    return true;
                }
                else return false;
            }
        }

        public async Task<bool> DeleteRole(string id)
        {
            var roleNow = await roleManager.FindByIdAsync(id);
            var result = await roleManager.DeleteAsync(roleNow);
            if (result.Succeeded) return true;
            else return false;
        }

        public List<IdentityRole> GetRoles()
        {
            return roleManager.Roles.ToList();
        }

        

        public async Task<IList<IdentityRole>> GetRolesByUser(string userId)
        {
            var user = userManager.Users.Where(u => u.Id == userId).FirstOrDefault();

            var roleNames = await userManager.GetRolesAsync(user);

            var roles = roleManager.Roles.Where(r => roleNames.Contains(r.Name)).ToList();

            return roles;
        }

        public async Task<bool> SetRole(string userId, List<string> rolesId)
        {
            var user = userManager.Users.Where(u => u.Id == userId).FirstOrDefault();
            if (user == null)
            {
                return false; // User not found
            }

            var currentRoles = await userManager.GetRolesAsync(user);
            var allRoles = roleManager.Roles.ToList();
            var rolesToAdd = new List<string>();
            var rolesToRemove = new List<string>();

            foreach (var role in allRoles)
            {
                if (rolesId.Contains(role.Id))
                {
                    if (!currentRoles.Contains(role.Name))
                    {
                        rolesToAdd.Add(role.Name);
                    }
                }
                else
                {
                    // nếu mà danh sachs roleid mà không có role đang duyệt này thì sẽ xóa
                    // xóa bằng cách kiểm tra nếu tất cả role của user này mà có cái role đang duyệt này thì mới thêm vào role remove 
                    // vì nếu tất cả role của user này mà không có cái role đang duyệt thì sẽ không xóa được
                    if (currentRoles.Contains(role.Name))
                    {
                        rolesToRemove.Add(role.Name);
                    }
                }
            }

            foreach (var roleName in rolesToRemove)
            {
                await userManager.RemoveFromRoleAsync(user, roleName);
            }

            foreach (var roleName in rolesToAdd)
            {
                await userManager.AddToRoleAsync(user, roleName);
            }

            return true;
        }

        public async Task<bool> UpdateRole(string roleId, string roleName)
        {
            var roleNow = await roleManager.FindByIdAsync(roleId);
            roleNow.Name = roleName;
            var result =await roleManager.UpdateAsync(roleNow);
            if (result.Succeeded)
            {
                return true;

            }
            else return false;
        }
    }
}
