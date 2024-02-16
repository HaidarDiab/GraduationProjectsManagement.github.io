using GraduationProjectsManagement.Models;
using GraduationProjectsManagement.Repositories.Cotracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace GraduationProjectsManagement.Pages.Supervisor
{
    [Authorize]
    public class ProjectsEvaluationsBase : ComponentBase
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
        public IEnumerable<IdentityUser> Supervisors { get; set; } = new List<IdentityUser>();
        public IEnumerable<ProjectEvaluation> ProjectEvaluations { get; set; } = new List<ProjectEvaluation>();
        public IEnumerable<ProjectGroup> ProjectsGroups { get; set; } = new List<ProjectGroup>();
        public IEnumerable<StudentGroup> StudentGroups { get; set; } = new List<StudentGroup>();
        public IEnumerable<Project> Projects { get; set; } = new List<Project>();

        public ProjectEvaluation ProjectEvaluation { get; set; } = new();


        public ClaimsPrincipal CurrentUser { get; set; } = new();

        public IdentityUser SupervisorOwnerForGroupProject { get; set; }

        public string? CurrentUserId { get; set; }
        public string? UserId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            CurrentUser = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
            var identityUser = await _userManager.GetUserAsync(CurrentUser);
            CurrentUserId = identityUser.Id;

            ProjectEvaluations = await ProjectRepository.GetProjectEvaluations();
            ProjectsGroups = await ProjectRepository.GetProjectGroups();
            StudentGroups = await ProjectRepository.GetStudentsGroups();
            Projects = await ProjectRepository.GetAllProjects();
            Users = await AccountRepository.GetAllUsers();

            SupervisorOwnerForGroupProject = (from projectGroup in ProjectsGroups
                                              join projectEvaluation in ProjectEvaluations on projectGroup.Id equals projectEvaluation.ProjectGroupId
                                              join user in Users on projectGroup.SupervisorId equals user.Id
                                              select user).FirstOrDefault();
        }


        

        protected async Task Delete(int projectEvaluationId)
        {
            bool confirmed = await JsRuntime.InvokeAsync<bool>("confirm", "هل أنت متأكد من إكمال عملية الحذف؟");


            if (confirmed)
            {
                    if (CurrentUser.IsInRole("Supervisor") && ProjectEvaluations.Any(pe => pe.Id == projectEvaluationId) && SupervisorOwnerForGroupProject.Id == CurrentUserId)
                    {
                    //if (ProjectsGroups.Any(pg => pg.Id == ProjectEvaluation.ProjectGroupId))
                    //{
                    //    await ProjectRepository.RemoveProjectGroup(ProjectsGroups.FirstOrDefault(pg => pg.Id == ProjectEvaluation.ProjectGroupId).Id);
                    //}
                        await ProjectRepository.RemoveProjectEvaluation(projectEvaluationId);

                        await JsRuntime.InvokeVoidAsync("alert", "تمت عملية الحذف بنجاح");
                    NavigationManager.NavigateTo("/ProjectsEvaluations", true);
                    }

                else await JsRuntime.InvokeVoidAsync("alert", "لا تمتلك الصلاحية لحذف هذا العنصر");
            }
        }

        protected async Task<bool> IsCurrentUserIsProjectGroupOwner()
        {
            var SupervisorOwnerForGroupProject = (from projectGroup in ProjectsGroups
                                              join projectEvaluation in ProjectEvaluations on projectGroup.Id equals projectEvaluation.ProjectGroupId
                                              join user in Users on projectGroup.SupervisorId equals user.Id
                                              select user).FirstOrDefault();
            if (SupervisorOwnerForGroupProject.Id == CurrentUserId)
                return true;

            else return false;
        }

    }
}
