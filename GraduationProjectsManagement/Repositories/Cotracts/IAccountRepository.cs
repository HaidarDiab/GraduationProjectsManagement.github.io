using GraduationProjectsManagement.Models;
using Microsoft.AspNetCore.Identity;

namespace GraduationProjectsManagement.Repositories.Cotracts
{
    public interface IAccountRepository
    {


        //...................................................................................//
        Task<IdentityUser> GetUser(Project project);
        Task<IdentityUser> GetUser(string userId);
        Task<IEnumerable<IdentityUser>> GetAllUsers();
        Task<IEnumerable<IdentityUser>> GetSupervisors();
        Task<IEnumerable<IdentityUser>> GetAllUsersByDiscussionBlogId(int discussionBlogId);
        Task RemoveUser(string userId);



        //...................................................................................//
        Task<IdentityUserRole<string>> GetUserRoleByUserId(string userId);
        Task<IEnumerable<IdentityUserRole<string>>> GetUsersRoles();
        Task<IEnumerable<IdentityRole>> GetRolesType();

        Task<IdentityRole> GetRolesTypeByRoleId(string roleId);

        Task AddUserRole(IdentityUserRole<string> userRole);
        Task RemoveUserRole(string userId);

        //...................................................................................//
    }
}
