using GraduationProjectsManagement.Models;
using GraduationProjectsManagement.Repositories.Cotracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace GraduationProjectsManagement.Pages
{
    public class ProjectDetailsBase : ComponentBase
    {
        [Parameter]
        public int ProjectId { get; set; }

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

        public IEnumerable<ProjectGroup> ProjectGroups { get; set; } = new List<ProjectGroup>();
        public IEnumerable<Project> Projects { get; set; } = new List<Project>();
        public IEnumerable<ProjectEvaluation> ProjectEvaluations { get; set; } = new List<ProjectEvaluation>();
        public IEnumerable<StudentGroup> StudentGroups { get; set; } = new List<StudentGroup>();

        public Project Project { get; set; }
        public IdentityUser User { get; set; }
        public ClaimsPrincipal CurrentUser { get; set; }


        public string UserId { get; set; }


        protected override async Task OnInitializedAsync()
        {
            CurrentUser = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
            var identityUser = await _userManager.GetUserAsync(CurrentUser);
            UserId = identityUser.Id;

            Project = await ProjectRepository.GetProjectById(ProjectId);
            User = await AccountRepository.GetUser(Project);

            Projects = await ProjectRepository.GetAllProjects();

            ProjectGroups = await ProjectRepository.GetProjectGroups();
            ProjectEvaluations = await ProjectRepository.GetProjectEvaluations();
            StudentGroups = await ProjectRepository.GetStudentsGroups();

        }

        protected async Task Delete(int projectId)
        {
            bool confirmed = await JsRuntime.InvokeAsync<bool>("confirm", "هل أنت متأكد من إكمال عملية الحذف؟");

            if (confirmed)
            {

                if (CurrentUser.IsInRole("Admin"))
                {

                    if (Projects.Any(p => p.Id == projectId))
                    {

                        if (ProjectGroups.Any(pg => pg.ProjectId == projectId))
                        {
                            var projectgroupIdByProjectId = ProjectGroups.SingleOrDefault(pg => pg.ProjectId == projectId).Id;

                            if (StudentGroups.Any(sg => sg.ProjectGroupId == projectgroupIdByProjectId))
                            {

                                foreach (var item in StudentGroups.Where(sg => sg.ProjectGroupId == projectgroupIdByProjectId).ToList())
                                {
                                    await ProjectRepository.RemoveStudentGroup(item.Id);
                                }
                            }

                            if (ProjectEvaluations.Any(pe => pe.ProjectGroupId == projectgroupIdByProjectId))
                            {
                                var projectevaluationIdByProjectGroupId = ProjectEvaluations.SingleOrDefault(pe => pe.ProjectGroupId == projectgroupIdByProjectId).Id;

                                await ProjectRepository.RemoveProjectEvaluation(projectevaluationIdByProjectGroupId);
                            }

                            await ProjectRepository.RemoveProjectGroup(projectgroupIdByProjectId);
                        }

                        await ProjectRepository.RemoveProject(projectId);

                        await JsRuntime.InvokeVoidAsync("alert", "تمت عملية الحذف بنجاح");

                        if (CurrentUser.IsInRole("Admin") || CurrentUser.IsInRole("Supervisor"))
                        {
                            NavigationManager.NavigateTo("/ProposalProjectsBySupervisors");

                        }
                        else NavigationManager.NavigateTo("/ProposalProjectsByStudents");
                    }

                    else await JsRuntime.InvokeVoidAsync("alert", "المشروع غير موجود");
                }

                else await JsRuntime.InvokeVoidAsync("alert", "لا تمتلك الصلاحية لحذف هذا العنصر");
            }



        }

    }
}
