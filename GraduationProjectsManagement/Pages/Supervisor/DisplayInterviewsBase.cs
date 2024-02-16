using GraduationProjectsManagement.Models;
using GraduationProjectsManagement.Repositories.Cotracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace GraduationProjectsManagement.Pages.Supervisor
{
    public class DisplayInterviewsBase : ComponentBase
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



        public IEnumerable<Interview> Interviews { get; set; } = new List<Interview>();
        public IEnumerable<Project> Projects { get; set; } = new List<Project>();

        public IEnumerable<ProjectGroup> ProjectGroups { get; set; } = new List<ProjectGroup>();
        public IEnumerable<ProjectEvaluation> ProjectEvaluations { get; set; } = new List<ProjectEvaluation>();
        public IEnumerable<StudentGroup> StudentGroups { get; set; } = new List<StudentGroup>();
        public IEnumerable<IdentityUser> Users { get; set; } = new List<IdentityUser>();

        public Interview Interview { get; set; } = new();
        public Project Project = new();
        public IdentityUser User = new();

        [Parameter]
        public int InterviewId { get; set; }

        public ClaimsPrincipal CurrentUser { get; set; }

        public string? UserId { get; set; }


        protected override async Task OnInitializedAsync()
        {

            CurrentUser = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
            var identityUser = await _userManager.GetUserAsync(CurrentUser);
            UserId = identityUser.Id;

            Interviews = await ProjectRepository.GetInterviews();
            Projects = await ProjectRepository.GetAllProjects();
            User = await AccountRepository.GetUser(Project);
            ProjectGroups = await ProjectRepository.GetProjectGroups();
            ProjectEvaluations = await ProjectRepository.GetProjectEvaluations();
            StudentGroups = await ProjectRepository.GetStudentsGroups();
            Users = await AccountRepository.GetAllUsers();  
        }


        protected async Task<IEnumerable<Project>> GetProjectFromInterviewStudentGroupIdAndStudentGroupAndProjectGroup(int studentGroupId)
        {
            return (from studentGroup in StudentGroups
                    join projectGroup in ProjectGroups on studentGroup.ProjectGroupId equals projectGroup.Id
                    join project in Projects on projectGroup.ProjectId equals project.Id
                    where studentGroup.Id == studentGroupId
                    select project).ToList();
        }

        protected async Task<IEnumerable<ProjectGroup>> GetProjectGroupFromInterviewStudentGroupIdAndStudentGroup(int studentGroupId)
        {
            return (from studentGroup in StudentGroups
                    join projectGroup in ProjectGroups on studentGroup.ProjectGroupId equals projectGroup.Id
                    where studentGroup.Id == studentGroupId
                    select projectGroup).ToList();
        }


        protected async Task Delete(int interviewId)
        {
            var interviewsInDb = await ProjectRepository.GetInterviews();

            bool confirmed = await JSRuntime.InvokeAsync<bool>("confirm", "هل أنت متأكد من إكمال عملية الحذف؟");

            if (confirmed)
            {
                if (interviewsInDb.Any(i => i.Id == interviewId))
                {
                    await ProjectRepository.RemoveInterview(interviewId);
                    await JSRuntime.InvokeVoidAsync("alert", "تمت عملية الحذف بنجاح");

                    NavigationManager.NavigateTo("/DisplayInterviews", true);
                }

                else
                {
                    await JSRuntime.InvokeVoidAsync("alert", "هذا العنصر غير متوفر");
                    NavigationManager.NavigateTo("/DisplayInterviews", true);
                }

            }
        }

    }
}
