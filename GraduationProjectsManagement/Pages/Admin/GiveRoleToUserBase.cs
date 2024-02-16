using GraduationProjectsManagement.Models;
using GraduationProjectsManagement.Pages.Public;
using GraduationProjectsManagement.Repositories.Cotracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace GraduationProjectsManagement.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class GiveRoleToUserBase : ComponentBase
    {

        [Inject]
        public IProjectRepository ProjectRepository { get; set; }

        [Inject]
        public IAccountRepository AccountRepository { get; set; }

        [Inject]
        public IJSRuntime JsRuntime { get; set; }


        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public IdentityUser User = new();
        public IEnumerable<IdentityRole> Roles { get; set; } = new List<IdentityRole>();
        public IEnumerable<IdentityUserRole<string>> UsersRoles { get; set; } = new List<IdentityUserRole<string>>();

        public IdentityRole Role = new();

        public IdentityUserRole<string> UserRole = new();

        [Parameter]
        public string? UserId { get; set; }

        public string? RoleId { get; set; }
        public string? Title { get; set; }

        protected override async Task OnInitializedAsync()
        {
            UsersRoles = await AccountRepository.GetUsersRoles();

            Roles = await AccountRepository.GetRolesType();
            User = await AccountRepository.GetUser(UserId);

                UserRole = await AccountRepository.GetUserRoleByUserId(UserId);

            if (UsersRoles.Any(ur => ur.UserId == UserId))
            {
                Title = "تحديث دور المستخدم";
                Role.Id = UserRole.RoleId;
            }
            else
            {
                Title = "إضافة دور للمستخدم";
                UserRole = new();
            }

        }

        public async Task Save()
        {
            UsersRoles = await AccountRepository.GetUsersRoles();

            try
            {

                if (!UsersRoles.Any(ur => ur.UserId == UserId))
                {
                    UserRole.UserId = UserId;
                    UserRole.RoleId = Role.Id;

                    await AccountRepository.AddUserRole(UserRole);

                    await JsRuntime.InvokeVoidAsync("alert", "تمت عملية الإضافة بنجاح");
                    NavigationManager.NavigateTo("/ManageUsers", true);


                }
                else
                {
 
                   await AccountRepository.RemoveUserRole(UserId);

                    UserRole.UserId = UserId;
                    UserRole.RoleId = Role.Id;

                

                    await AccountRepository.AddUserRole(UserRole);

                    await JsRuntime.InvokeVoidAsync("alert", "تمت عملية التحديث بنجاح");

                    NavigationManager.NavigateTo("/ManageUsers", true);

                }
            }
            catch (Exception)
            {
                await JsRuntime.InvokeVoidAsync("alert", "حدث خطأ الرجاء المحاولة مجدداً");
            }

        }

        public async Task Cancel()
        {
            await ClearLocalStorage();

            NavigationManager.NavigateTo("/ManageUsers");
        }

        private async Task ClearLocalStorage()
        {
            await ProjectRepository.RemoveCollections();
        }

    }
}
