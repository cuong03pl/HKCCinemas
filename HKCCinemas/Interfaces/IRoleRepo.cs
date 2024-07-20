using Microsoft.AspNetCore.Identity;

namespace HKCCinemas.Interfaces
{
    public interface IRoleRepo
    {
        Task<bool> CreateRole(string roleName);
        Task<bool> DeleteRole(string id);
        Task<bool> UpdateRole(string roleId,string roleName);
        Task<IList<IdentityRole>> GetRolesByUser(string userId);
        List<IdentityRole> GetRoles();

        Task<bool> SetRole(string userId, List<string> rolesId);
    }
}
