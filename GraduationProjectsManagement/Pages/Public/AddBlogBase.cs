using GraduationProjectsManagement.Models;
using GraduationProjectsManagement.Repositories.Cotracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace GraduationProjectsManagement.Pages.Public
{
    public class AddBlogBase : ComponentBase
    {
        [Inject]
        public IBlogRepository BlogRepository { get; set; }

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


        public IEnumerable<Blog> Blogs { get; set; } = new List<Blog>();
        public IEnumerable<Project> Projects { get; set; } = new List<Project>();

        public IEnumerable<ProjectGroup> ProjectGroups { get; set; } = new List<ProjectGroup>();
        public IEnumerable<StudentGroup> StudentGroups { get; set; } = new List<StudentGroup>();

        public ProjectGroup ProjectGroup { get; set; } = new();
        public Blog Blog { get; set; } = new();
        public Project Project = new();
        public IdentityUser User = new();

        [Parameter]
        public int blogId { get; set; }

        public ClaimsPrincipal CurrentUser { get; set; }


        //maybe Student or Supervisor Or Admin Id
        public string? CurrentUserId { get; set; }

        public string? Title { get; set; }


        protected override async Task OnInitializedAsync()
        {

            CurrentUser = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
            var identityUser = await _userManager.GetUserAsync(CurrentUser);
            CurrentUserId = identityUser.Id;

            Projects = await ProjectRepository.GetAllProjects();
            User = await AccountRepository.GetUser(Project);

            
            if (CurrentUser.IsInRole("Admin"))
            {
                ProjectGroups = await ProjectRepository.GetProjectGroups();
                StudentGroups = await ProjectRepository.GetStudentsGroups();
                Blogs = await BlogRepository.GetAllBlogs();

                Blog = await BlogRepository.GetBlogById(blogId);
            }
            
            else if (CurrentUser.IsInRole("Student"))
            {
                ProjectGroups = await ProjectRepository.GetProjectGroupsForSpecificStudent(CurrentUserId);

                StudentGroups = await ProjectRepository.GetStudentGroupByStudentId(CurrentUserId);
                Blogs = await BlogRepository.GetAllBlogs();

                Blog = await BlogRepository.GetBlogById(blogId);

            }
            else if (CurrentUser.IsInRole("Supervisor"))
            {
                ProjectGroups = await ProjectRepository.GetProjectGroupsForSpecificSupervisor(CurrentUserId);

                StudentGroups = await ProjectRepository.GetStudentGroupByGroupId(
                    ProjectRepository.GetProjectGroupsForSpecificSupervisor(CurrentUserId).Id);

                Blogs = await BlogRepository.GetAllBlogs();

                Blog = await BlogRepository.GetBlogById(blogId);
            }

               if (blogId == 0)
                {
                    Title = "إضافة نقاش";
                    Blog = new();
                }

                else
                {
                    Title = "تعديل نقاش";
                    Blog.Title = Blogs.FirstOrDefault(b => b.Id == blogId).Title;
                    Blog.Question = Blogs.FirstOrDefault(b => b.Id == blogId).Question;

                }
        }



        protected async Task Save()
        {
            if (blogId == 0) 
            {

                if (Blogs.Any(b => b.Title == Blog.Title))
                {
                    await JSRuntime.InvokeVoidAsync("alert", "هذا النقاش مطروح سابقاً");
                }

                else
                {
                    Blog.UserId = CurrentUserId;

                    await BlogRepository.AddBlog(Blog);
                    await JSRuntime.InvokeVoidAsync("alert", "تمت عملية الإضافة بنجاح");
                    NavigationManager.NavigateTo("/DisplayBlogs");
                }
            }

            else
            {
                await BlogRepository.UpdateBlog(Blog);
                await JSRuntime.InvokeVoidAsync("alert", "تمت عملية التحديث بنجاح");
                NavigationManager.NavigateTo("/DisplayBlogs");
            }
        }

        public void Cancel()
        {
            NavigationManager.NavigateTo("");
        }

    }
}
