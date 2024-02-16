using GraduationProjectsManagement.Repositories.Cotracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using GraduationProjectsManagement.Models;


namespace GraduationProjectsManagement.Pages.Public
{
    [Authorize]
    public class AddProposalProjectBase : ComponentBase
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
        public UserManager<IdentityUser> _userManager { get; set; }


        [Inject]
        public AuthenticationStateProvider _authenticationStateProvider { get; set; }
        public IEnumerable<Project> Projects { get; set; } = Enumerable.Empty<Project>();
        public IEnumerable<ProjectType> ProjectTypes { get; set; } = Enumerable.Empty<ProjectType>();
        public IEnumerable<ProjectCategory> ProjectCategories { get; set; } = Enumerable.Empty<ProjectCategory>();

        public ClaimsPrincipal User { get; set; }

        [Parameter]
        public int ProjectId { get; set; }


        public Project Project = new Project();

        protected string currentUserId { get; set; }

        public string? Title { get; set; }
        public string? UrlNavigation { get; set; }

        private byte[] imageData;

        protected override async Task OnInitializedAsync()
        {
            User = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
            var identityUser = await _userManager.GetUserAsync(User);
            var userId = identityUser.Id;

            ProjectCategories = await ProjectRepository.GetProjectCategories();
            ProjectTypes = await ProjectRepository.GetProjectTypes();
            currentUserId = userId;


            if (ProjectId != 0)
            {
                var projects = await ProjectRepository.GetAllProjects();

                Title = "تحديث مشروع";
                UrlNavigation = $"/ProjectDetails/{ProjectId}";
                Project = await ProjectRepository.GetProjectById(ProjectId);
                Project.Id = ProjectId;
                Project.Title = Project.Title;
                Project.Description = Project.Description;
                Project.ProjectCategoryId = Project.ProjectCategoryId;
            }

            else
            {
                var rolesType = await AccountRepository.GetRolesType();
                var userRole = await AccountRepository.GetUsersRoles();
            
                if(userRole.Any(r => r.UserId == currentUserId))
                {
                    UrlNavigation = "/ProposalProjectsBySupervisors";

                }
                else if (rolesType.Any(r => r.Name == "Student"))
                {
                    UrlNavigation = "/ProposalProjectsByStudents";

                }


                Title = "إضافة مقترح مشروع";
                Project = new();
            }
        }


        public async Task Save()
        {
            Projects = await ProjectRepository.GetAllProjects();

            try
            {

                if (ProjectId == 0)
                {
                    if (!Projects.Any(p => p.Title == Project.Title))
                    {

                        Project.UserId = currentUserId;
                        await ProjectRepository.AddProject(Project);

                        await JSRuntime.InvokeVoidAsync("alert", "تمت عملية الإضافة بنجاح");

                        NavigationManager.NavigateTo(UrlNavigation, true);
                    }
                }

                else
                {

                    if (Project.UserId == currentUserId || User.IsInRole("Admin"))
                    {

                        await ProjectRepository.UpdateProject(Project);
                        await JSRuntime.InvokeVoidAsync("alert", "تمت عملية التحديث بنجاح");


                        NavigationManager.NavigateTo($"{UrlNavigation}");
                    }
                    else
                    {
                        await JSRuntime.InvokeVoidAsync("alert", "لا تملك صلاحية التعديل على مشروع لم تقم أنت بإضافته");
                    }
                }
            }
            catch (Exception)
            {
                await JSRuntime.InvokeVoidAsync("alert", "حدث خطأ الرجاء المحاولة مجدداً");

            }

        }

        protected async Task LoadImage(InputFileChangeEventArgs e)
        {
            var file = e.File;
            using (var stream = file.OpenReadStream())
            {
                imageData = new byte[stream.Length];
                await stream.ReadAsync(imageData, 0, (int)stream.Length);
            }
            Project.Image = "";
            Project.ImageFile = imageData;

        }

        public void Cancel()
        {
            NavigationManager.NavigateTo("");
        }

        private async Task ClearLocalStorage()
        {
            await ProjectRepository.RemoveCollections();
        }
    }
}
