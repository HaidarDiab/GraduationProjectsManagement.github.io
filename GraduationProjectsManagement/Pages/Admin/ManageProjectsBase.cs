using GraduationProjectsManagement.Models;
using GraduationProjectsManagement.Pages.Public;
using GraduationProjectsManagement.Repositories.Cotracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace GraduationProjectsManagement.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class ManageProjectsBase : ComponentBase
    {
        [Inject]
        public IProjectRepository ProjectRepository { get; set; }

        [Inject]
        public IAccountRepository AccountRepository { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public AuthenticationStateProvider _authenticationStateProvider { get; set; }

        [Inject]
        public UserManager<IdentityUser> _userManager { get; set; }


        public IEnumerable<Project> Projects { get; set; } = new List<Project>();

        public IEnumerable<ProjectGroup> ProjectGroups { get; set; } = new List<ProjectGroup>();
        public IEnumerable<ProjectEvaluation> ProjectEvaluations { get; set; } = new List<ProjectEvaluation>();
        public IEnumerable<StudentGroup> StudentGroups { get; set; } = new List<StudentGroup>();


        public Project Project = new();
        public IdentityUser User = new();

        [Parameter]
        public int ProjectId { get; set; }

        public ClaimsPrincipal CurrentUser { get; set; }

        public string? UserId { get; set; }


        protected override async Task OnInitializedAsync()
        {
            
            CurrentUser = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
            var identityUser = await _userManager.GetUserAsync(CurrentUser);
            UserId = identityUser.Id;

            Projects = await ProjectRepository.GetAllProjects();
            User = await AccountRepository.GetUser(Project);
            ProjectGroups = await ProjectRepository.GetProjectGroups();
            ProjectEvaluations = await ProjectRepository.GetProjectEvaluations();
            StudentGroups = await ProjectRepository.GetStudentsGroups();
        }


        protected async Task Delete(int projectId)
        {
            var projectIsExisting = Projects.Any(p => p.Id == projectId);

            bool confirmed = await JSRuntime.InvokeAsync<bool>("confirm", "هل أنت متأكد من إكمال عملية الحذف؟");

            if (confirmed)
            {
                if (CurrentUser.IsInRole("Admin") && projectIsExisting)
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

                    await JSRuntime.InvokeVoidAsync("alert", "تمت عملية الحذف بنجاح");

                    NavigationManager.NavigateTo("/ManageProjects", true);
                }

                else
                {
                    await JSRuntime.InvokeVoidAsync("alert", "هذا المشروع غير متوفر");
                    NavigationManager.NavigateTo("/ManageProjects", true);
                }
            }
        }

        protected async Task SelectProjectInThisTerm(int projectId)
        {
            var projectIsExisting = Projects.Any(p => p.Id == projectId);
            var projectInDb = await ProjectRepository.GetProjectById(projectId);
            bool confirmed = await JSRuntime.InvokeAsync<bool>("confirm", "هل أنت متأكد من إكمال عملية الاختيار؟");

            if (confirmed)
            {
                if (CurrentUser.IsInRole("Admin") && projectIsExisting && projectInDb.IsSelected == false)
                {
                    Project.Id = projectId;
                    Project.Title = projectInDb.Title;
                    Project.AddedDate = projectInDb.AddedDate;
                    Project.Description = projectInDb.Description;
                    Project.ProjectCategoryId = projectInDb.ProjectCategoryId;
                    Project.ProjectTypeId = projectInDb.ProjectTypeId;
                    Project.Image = projectInDb.Image;
                    Project.ImageFile = projectInDb.ImageFile;
                    Project.UserId = projectInDb.UserId;
                    Project.IsSelected = true;
                    await ProjectRepository.UpdateProject(Project);

                    await JSRuntime.InvokeVoidAsync("alert", "تمت عملية الاختيار بنجاح");

                    NavigationManager.NavigateTo("/ManageProjects", true);
                }

                else
                {
                    await JSRuntime.InvokeVoidAsync("alert", "هذا المشروع غير متوفر أو تم اختياره مسبقاً");
                    NavigationManager.NavigateTo("/ManageProjects", true);
                }
            }
        }

        protected async Task UnSelectProjectInThisTerm(int projectId)
        {
            var projectIsExisting = Projects.Any(p => p.Id == projectId);
            var projectInDb = await ProjectRepository.GetProjectById(projectId);
            bool confirmed = await JSRuntime.InvokeAsync<bool>("confirm", "هل أنت متأكد من إلغاء اختيار المشروع كمشروع معتمد في هذا الفصل؟");

            if (confirmed)
            {
                if (CurrentUser.IsInRole("Admin") && projectIsExisting && projectInDb.IsSelected == true)
                {
                    Project.Id = projectId;
                    Project.Title = projectInDb.Title;
                    Project.AddedDate = projectInDb.AddedDate;
                    Project.Description = projectInDb.Description;
                    Project.ProjectCategoryId = projectInDb.ProjectCategoryId;
                    Project.ProjectTypeId = projectInDb.ProjectTypeId;
                    Project.Image = projectInDb.Image;
                    Project.ImageFile = projectInDb.ImageFile;
                    Project.UserId = projectInDb.UserId;
                    Project.IsSelected = false;
                    await ProjectRepository.UpdateProject(Project);

                    await JSRuntime.InvokeVoidAsync("alert", "تمت عملية الإلغاء بنجاح");

                    NavigationManager.NavigateTo("/ManageProjects", true);
                }

                else
                {
                    await JSRuntime.InvokeVoidAsync("alert", "هذا المشروع غير متوفر أو لم يتم اختياره مسبقاً");
                    NavigationManager.NavigateTo("/ManageProjects", true);
                }
            }
        }
    }
}
