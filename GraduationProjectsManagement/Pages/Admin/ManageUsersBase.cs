using GraduationProjectsManagement.Models;
using GraduationProjectsManagement.Repositories.Cotracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using System.Collections.Immutable;
using System.Security.Claims;

namespace GraduationProjectsManagement.Pages.Admin
{
    [Authorize(Roles ="Admin")]
    public class ManageUsersBase : ComponentBase
    {
        
        [Inject]
        public IProjectRepository ProjectRepository { get; set; }


        [Inject]
        public IAccountRepository AccountRepository { get; set; }


        [Inject]
        public IJSRuntime JsRuntime { get; set; }


        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public AuthenticationStateProvider _authenticationStateProvider { get; set; }

        [Inject]
        public UserManager<IdentityUser> _userManager { get; set; }

        public IEnumerable<IdentityUser> Users { get; set; } = new List<IdentityUser>();
        public IEnumerable<ProjectGroup> ProjectGroups { get; set; } = new List<ProjectGroup>();
        public IEnumerable<IdentityRole> Roles { get; set; } = new List<IdentityRole>();
        public IEnumerable<IdentityUserRole<string>> UsersRoles { get; set; } = new List<IdentityUserRole<string>>();


        public ClaimsPrincipal CurrentUser { get; set; } = new();

        public string? CurrentUserId { get; set; }
        public string? UserId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            CurrentUser = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
            var identityUser = await _userManager.GetUserAsync(CurrentUser);
            CurrentUserId = identityUser.Id;

            Users = await AccountRepository.GetAllUsers();
            Roles = await AccountRepository.GetRolesType();
            UsersRoles = await AccountRepository.GetUsersRoles();
            ProjectGroups = await ProjectRepository.GetProjectGroups();
        }

        protected string GetRoleName(string userId)
        {
            var roleFromUsersRoles = from role in Roles
                                     join userRole in UsersRoles on role.Id equals userRole.RoleId
                                     where userRole.UserId == userId && role.Id == userRole.RoleId
                                     select role.Name;

            return roleFromUsersRoles.FirstOrDefault();
        }

        protected async Task Delete(string userId)
        {
            bool confirmed = await JsRuntime.InvokeAsync<bool>("confirm", "هل أنت متأكد من إكمال عملية الحذف؟");
            var studentsGroups = await ProjectRepository.GetStudentsGroups();

            if (confirmed)
            {
                if (CurrentUser.IsInRole("Admin"))
                {
                    if (ProjectGroups.Any(pg => pg.SupervisorId == userId))
                    {
                        var projectGroupId = ProjectGroups.Where(pg => pg.SupervisorId == userId).FirstOrDefault().Id;
                        await ProjectRepository.RemoveProjectGroup(projectGroupId);
                    }

                    if (studentsGroups.Any(sg => sg.StudentId == userId))
                    {
                    await ProjectRepository.RemoveStudentGroup(studentsGroups.FirstOrDefault(sg => sg.StudentId == userId).Id);

                    }
                    if (UsersRoles.Any(ur => ur.UserId == userId))
                    {

                    await AccountRepository.RemoveUserRole(userId);
                    }

                    await AccountRepository.RemoveUser(userId);

                    await JsRuntime.InvokeVoidAsync("alert", "تمت عملية الحذف بنجاح");
                    await ClearLocalStorage();

                    NavigationManager.NavigateTo("/ManageUsers", true);
                }

                else await JsRuntime.InvokeVoidAsync("alert", "لا تمتلك الصلاحية لحذف هذا العنصر");
            }

        }

        protected async Task RoleCancel(string userId)
        {
            bool confirmed = await JsRuntime.InvokeAsync<bool>("confirm", "هل أنت متأكد من إكمال عملية إلغاء دور المستخدم في الموقع؟");

            if (confirmed)
            {
                if (CurrentUser.IsInRole("Admin"))
                {
                    if (UsersRoles.Any(ur => ur.UserId == userId))
                    {
                        
                    await AccountRepository.RemoveUserRole(userId);

                    await JsRuntime.InvokeVoidAsync("alert", "تمت عملية إلغاء الدور بنجاح");
                    await ClearLocalStorage();

                    NavigationManager.NavigateTo("/ManageUsers", true);
                    }
                    else
                    {
                        await JsRuntime.InvokeVoidAsync("alert", "لا يوجد له دور حتى يتم إلغاءه");
                        await OnInitializedAsync();
                    }
                }
            }

            else await JsRuntime.InvokeVoidAsync("alert", "لا تمتلك الصلاحية لإلغاء دور المستحدم");

        }

        private async Task ClearLocalStorage()
        {
            await ProjectRepository.RemoveCollections();
        }


    }
}
