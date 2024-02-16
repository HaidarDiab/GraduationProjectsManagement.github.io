using GraduationProjectsManagement.Models;
using GraduationProjectsManagement.Repositories.Cotracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace GraduationProjectsManagement.Pages.Public
{
    [Authorize]
    public class DisplayMyProjectGroupsBase : ComponentBase
    {
        [Inject]
        public IProjectRepository ProjectRepository { get; set; }


        [Inject]
        public IAccountRepository AccountRepository { get; set; }


        [Inject]
        public UserManager<IdentityUser> _userManager { get; set; }


        [Inject]
        public AuthenticationStateProvider _authenticationStateProvider { get; set; }

        public IEnumerable<ProjectGroup> ProjectsGroups { get; set; } = Enumerable.Empty<ProjectGroup>();
        public IEnumerable<ProjectGroup> MyProjectsGroups { get; set; } = Enumerable.Empty<ProjectGroup>();
        public IEnumerable<StudentGroup> StudentsGroups { get; set; } = Enumerable.Empty<StudentGroup>();
        public IEnumerable<Project> Projects { get; set; } = Enumerable.Empty<Project>();
        public IEnumerable<IdentityUser> Supervisors { get; set; } = Enumerable.Empty<IdentityUser>();

        public ClaimsPrincipal User { get; set; } = new();


        [Parameter]
        public string? currentUserId { get; set; }


        protected async override Task OnInitializedAsync()
        {
            User = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
            var identityUser = await _userManager.GetUserAsync(User);
            currentUserId = identityUser.Id;

            Projects = await ProjectRepository.GetSelectedProjects();
            Supervisors = await AccountRepository.GetSupervisors();
            StudentsGroups = await ProjectRepository.GetStudentsGroups();

            ProjectsGroups = await ProjectRepository.GetProjectGroups();

            if (ProjectsGroups.Any(pg => pg.SupervisorId == currentUserId))
            {
                MyProjectsGroups = await ProjectRepository.GetProjectGroupsForSpecificSupervisor(currentUserId);

            }
            else
            {
            MyProjectsGroups = await ProjectRepository.GetProjectGroupsForSpecificStudent(currentUserId);

            }
        }

     
    }
}
