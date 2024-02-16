using Blazored.LocalStorage;
using GraduationProjectsManagement.Data;
using GraduationProjectsManagement.Models;
using GraduationProjectsManagement.Repositories.Cotracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GraduationProjectsManagement.Repositories
{
    public class AccountRepository : IAccountRepository
    {


        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private readonly ILocalStorageService _localStorageService;
        private readonly ApplicationDbContext dbContext;

        private const string key = "ProjectCollection";
        public AccountRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory, ILocalStorageService localStorageService)
        {
            _dbContextFactory = dbContextFactory;
            _localStorageService = localStorageService;
            dbContext = _dbContextFactory.CreateDbContext();
        }



        //....................................//Roles............................................//

        public async Task<IdentityUserRole<string>> GetUserRoleByUserId(string userId)
        {
            return await dbContext.UserRoles.FirstOrDefaultAsync(us => us.UserId == userId);
        }

        public async Task<IEnumerable<IdentityUserRole<string>>> GetUsersRoles()
        {

            return await dbContext.UserRoles.ToListAsync();
        }

        public async Task<IEnumerable<IdentityRole>> GetRolesType()
        {
            //using var dbContext = _dbContextFactory.CreateDbContext();

            return await dbContext.Roles.ToListAsync();
        }

        public async Task<IdentityRole> GetRolesTypeByRoleId(string roleId)
        {
            //using var dbContext = _dbContextFactory.CreateDbContext();

            return await dbContext.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
        }


        //AddAnd Update at the same time beacuse IdentityUserRole table has tow key (userid and role id)
        //and in order to update  one of them we need to delete all row in the table then add new userRole object
        public async Task AddUserRole(IdentityUserRole<string> userRole)
        {
            var newUserRole = new IdentityUserRole<string>()
            {
                UserId = userRole.UserId,
                RoleId = userRole.RoleId,
            };

            await dbContext.UserRoles.AddAsync(newUserRole);
            await dbContext.SaveChangesAsync();
        }


        public async Task RemoveUserRole(string userId)
        {
            //using var dbContext = _dbContextFactory.CreateDbContext();

            var userRole = await dbContext.UserRoles.FirstOrDefaultAsync(u => u.UserId == userId);

            dbContext.UserRoles.Remove(userRole);
            await dbContext.SaveChangesAsync();
        }

        //....................................//End Of Roles............................................//



        //....................................//Users............................................//

        public async Task<IdentityUser> GetUser(Project Project)
        {
            //using var dbContext = _dbContextFactory.CreateDbContext();

            return await dbContext.Users.FirstOrDefaultAsync(u => u.Id == Project.UserId);
        }

        public async Task<IdentityUser> GetUser(string userId)
        {
            //using var dbContext = _dbContextFactory.CreateDbContext();

            return await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<IEnumerable<IdentityUser>> GetSupervisors()
        {
            //using var dbContext = _dbContextFactory.CreateDbContext();

            var supervisors = from user in dbContext.Users
                              join userRole in dbContext.UserRoles on user.Id equals userRole.UserId
                              join role in dbContext.Roles on userRole.RoleId equals role.Id
                              where role.Name == "Supervisor"
                              select user;
            return supervisors;
        }

        public async Task<IEnumerable<IdentityUser>> GetAllUsers()
        {
            //using var dbContext = _dbContextFactory.CreateDbContext();

            return await dbContext.Users.ToListAsync();
        }

        public async Task<IEnumerable<IdentityUser>> GetAllUsersByDiscussionBlogId(int discussionBlogId)
        {
            //using var dbContext = _dbContextFactory.CreateDbContext();
            var discussionBlog = await dbContext.DiscussionBlogs.FirstOrDefaultAsync(db => db.Id == discussionBlogId);
            return await dbContext.Users.Where(u => u.Id == discussionBlog.UserId).ToListAsync();
        }

        public async Task RemoveUser(string userId)
        {
            //using var dbContext = _dbContextFactory.CreateDbContext();

            var user = await dbContext.Users.FirstAsync(u => u.Id == userId);

            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
        }

  

        //....................................//End Of Users............................................//



    }
}


//private readonly ApplicationDbContext dbContext;
//private readonly ILocalStorageService _localStorageService;

//private const string key = "ProjectCollection";
//public AccountRepository(ApplicationDbContext context, ILocalStorageService localStorageService)
//{
//    dbContext = context;
//    _localStorageService = localStorageService;
//}







////....................................//Roles............................................//


//public async Task<IEnumerable<IdentityUserRole<string>>> GetRoles()
//{
//    return await dbContext.UserRoles.ToListAsync();
//}

//public async Task<IEnumerable<IdentityRole>> GetRolesType()
//{
//    return await dbContext.Roles.ToListAsync();
//}


//public async Task AddRole(string userId, string roleId)
//{
//    var userRole = new IdentityUserRole<string>()
//    {
//        RoleId = roleId,
//        UserId = userId
//    };

//    await dbContext.UserRoles.AddAsync(userRole);
//    await dbContext.SaveChangesAsync();
//}


//public async Task UpdateRole(string userId, string roleId)
//{
//    var userRoleInDb = await dbContext.UserRoles.FirstAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

//    userRoleInDb.RoleId = roleId;
//    userRoleInDb.UserId = userId;

//    await dbContext.SaveChangesAsync();
//}

//public async Task RemoveRole(string userId)
//{
//    var userRole = await dbContext.UserRoles.FirstAsync(u => u.UserId == userId);

//    dbContext.UserRoles.Remove(userRole);
//    dbContext.SaveChanges();
//}

////....................................//End Of Roles............................................//



////....................................//Users............................................//

//public async Task<IdentityUser> GetUser(Project Project)
//{
//    return await dbContext.Users.FirstOrDefaultAsync(u => u.Id == Project.UserId);
//}

//public async Task<IdentityUser> GetUser(string userId)
//{
//    return await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
//}

//public async Task<IEnumerable<IdentityUser>> GetSupervisors()
//{
//    var supervisors = from user in dbContext.Users
//                      join userRole in dbContext.UserRoles on user.Id equals userRole.UserId
//                      join role in dbContext.Roles on userRole.RoleId equals role.Id
//                      where role.Name == "Supervisor"
//                      select user;
//    return supervisors;
//}

//public async Task<IEnumerable<IdentityUser>> GetAllUsers()
//{
//    return await dbContext.Users.ToListAsync();
//}

//public async Task RemoveUser(string userId)
//{
//    var user = await dbContext.Users.FirstAsync(u => u.Id == userId);

//    dbContext.Users.Remove(user);
//    dbContext.SaveChanges();
//}



////....................................//End Of Users............................................//

