using GraduationProjectsManagement.Models;
using GraduationProjectsManagement.Repositories;
using GraduationProjectsManagement.Repositories.Cotracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace GraduationProjectsManagement.Pages.Public
{
    public class DisplayProjectsGroupsBase : ComponentBase
    {
        [Inject]
        public IProjectRepository ProjectRepository { get; set; }

        [Inject]
        public IBlogRepository BlogRepository { get; set; }


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

        public IEnumerable<ProjectGroup> ProjectsGroups { get; set; } = Enumerable.Empty<ProjectGroup>();
        public IEnumerable<StudentGroup> StudentsGroups { get; set; } = Enumerable.Empty<StudentGroup>();
        public IEnumerable<Project> Projects { get; set; } = Enumerable.Empty<Project>();

        public IEnumerable<Blog> Blogs { get; set; } = Enumerable.Empty<Blog>();
        public IEnumerable<IdentityUser> Supervisors { get; set; } = Enumerable.Empty<IdentityUser>();

        public ClaimsPrincipal User { get; set; } = new();

        public StudentGroup StudentGroup { get; set; } = new();
        public ProjectGroup ProjectGroup { get; set; } = new();

        [Parameter]
        public int GroupId { get; set; }
        public int ProjectId { get; set; }
        public string? SupervisorId { get; set; }

        protected string? currentUserId { get; set; }


        protected async override Task OnInitializedAsync()
        {            
            User = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
            var identityUser = await _userManager.GetUserAsync(User);
            currentUserId = identityUser.Id;

            ProjectsGroups = await ProjectRepository.GetProjectGroups();

            Projects = await ProjectRepository.GetSelectedProjects();
            Supervisors = await AccountRepository.GetSupervisors();


            StudentsGroups = await ProjectRepository.GetStudentsGroups();
            Blogs = await BlogRepository.GetAllBlogs();
        }

        protected async Task SelectProjectGroup(int groupId)
        {
            bool confirmed = await JSRuntime.InvokeAsync<bool>("confirm", "هل أنت متأكد من أنك تريد احتيار المجموعة؟");

            try
            {
                if (!User.IsInRole("Admin") && !User.IsInRole("Supervisor"))
                {
                    if (StudentsGroups.ToList().Count(p => p.ProjectGroupId == groupId) < 3)
                    {
                        if (!StudentsGroups.Any(sg => sg.StudentId == currentUserId))
                        {
                            StudentGroup.StudentId = currentUserId;
                            StudentGroup.ProjectGroupId = groupId;
                            StudentGroup.Name = ProjectsGroups.FirstOrDefault(sg => sg.Id == groupId).Name;

                            if (confirmed)
                            {
                            await ProjectRepository.AddStudentGroup(StudentGroup);

                            await JSRuntime.InvokeVoidAsync("alert", "تمت عملية اختيار المجموعة بنجاح");

                            }
                            NavigationManager.NavigateTo("/");
                            NavigationManager.NavigateTo("/DisplayProjectsGroups");
                        }

                        else
                        {
                            await JSRuntime.InvokeVoidAsync("alert", "لا يمكنك الاختيار لأنك قمت باختيار مجموعة مشروع مسبقاً");

                        }
                    }
                    else
                    {
                        await JSRuntime.InvokeVoidAsync("alert", "اكتمل عدد المشاركين في المجموعة لايمكنك الاشتراك بها");

                    }

                }

                else
                {
                    await JSRuntime.InvokeVoidAsync("alert", "لا يمكنك الاختيار لأنك مشرف أو مسؤول ");
                }
            }
            catch (Exception)
            {

                await JSRuntime.InvokeVoidAsync("alert", "حدث خطأ الرجاء المحاولة مجدداً");

            }
        }

        protected async Task UnSelectProjectGroup(int groupId)
        {
            var studentGroup = await ProjectRepository.GetStudentGroupByGroupId(groupId);

            bool confirmed = await JSRuntime.InvokeAsync<bool>("confirm", "هل أنت متأكد من إكمال عملية إلغاء اختيار المجموعة؟");


            if (studentGroup != null)
            {
                var studentGroupId = studentGroup.FirstOrDefault(s => s.ProjectGroupId == groupId && s.StudentId == currentUserId).Id;

                var studentBlogs = Blogs.Where(b => b.ProjectGroupId == studentGroupId).ToList();
                if (confirmed) 
                {
                    await ProjectRepository.RemoveStudentGroup(studentGroupId);
                    await JSRuntime.InvokeVoidAsync("alert", "تمت عملية الإلغاء بنجاح");

                    NavigationManager.NavigateTo("/");
                    NavigationManager.NavigateTo("/DisplayProjectsGroups");
                }
            }
        }

        protected async Task DeleteProjectGroup(int projectGroupId)
        {
            var projectGroup = await ProjectRepository.GetProjectGroupByGroupId(projectGroupId);
            var projectGroupsEvaluations = await ProjectRepository.GetProjectEvaluations();
            var projectGroupEvaluationIdByProjectGroupId = await ProjectRepository.GetSpecificProjectEvaluations(projectGroupId);

            //var projectGroupEvaluationIdByProjectGroupId = projectGroupsEvaluations.FirstOrDefault(pe => pe.ProjectGroupId == projectGroupId).Id;

            bool confirmed = await JSRuntime.InvokeAsync<bool>("confirm", "هل أنت متأكد من إكمال عملية حذف المجموعة؟");


            if (projectGroup != null && StudentsGroups != null)
            {

                if (confirmed)
                {
                    if (projectGroupsEvaluations.Any(pe => pe.ProjectGroupId == projectGroupId))
                    {
                        await ProjectRepository.RemoveProjectEvaluation(projectGroupEvaluationIdByProjectGroupId.Id);
                    }

                    foreach(var item in StudentsGroups.Where(sg => sg.ProjectGroupId == projectGroupId).ToList())
                    {
                        await ProjectRepository.RemoveStudentGroup(item.Id);
                    }
                    await ProjectRepository.RemoveProjectGroup(projectGroup.Id);

                    await JSRuntime.InvokeVoidAsync("alert", "تمت عملية الحذف بنجاح");

                    NavigationManager.NavigateTo("/");
                    NavigationManager.NavigateTo("/DisplayProjectsGroups");
                }
            }
        }
    }
}
