using GraduationProjectsManagement.Models;
using GraduationProjectsManagement.Repositories.Cotracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace GraduationProjectsManagement.Pages.Public
{
    public class DisplayBlogsBase : ComponentBase
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
        public IEnumerable<DiscussionBlog> DiscussionsBlogs { get; set; } = new List<DiscussionBlog>();

        public IEnumerable<ProjectGroup> ProjectGroups { get; set; } = new List<ProjectGroup>();
        public IEnumerable<StudentGroup> StudentGroups { get; set; } = new List<StudentGroup>();

        public StudentGroup StudentGroup { get; set; } = new();
        public Blog Blog { get; set; } = new();

        public IdentityUser User = new();

        //public int blogId { get; set; }

        public ClaimsPrincipal CurrentUser { get; set; }


        //maybe Student or Supervisor Or Admin Id
        [Parameter]
        public string? CurrentUserId { get; set; }

        public string? Title { get; set; }


        protected override async Task OnInitializedAsync()
        {

            CurrentUser = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
            var identityUser = await _userManager.GetUserAsync(CurrentUser);
            CurrentUserId = identityUser.Id;

            ProjectGroups = await ProjectRepository.GetProjectGroups();


            StudentGroups = await ProjectRepository.GetStudentsGroups();


            if (CurrentUser.IsInRole("Admin"))
            {
                Blogs = await BlogRepository.GetAllBlogs();
            }

            else if (CurrentUser.IsInRole("Supervisor"))
            {
                ProjectGroups = await ProjectRepository.GetProjectGroupsForSpecificSupervisor(CurrentUserId);

                var ifExistBlogsForCurrentUser = await BlogRepository.GetAllBlogsForCurrentUserByUserId(CurrentUserId);

                var blogs = await BlogRepository.GetAllBlogs();

       
                Blogs = !ifExistBlogsForCurrentUser.Any() ?
                        Enumerable.Empty<Blog>() :
                        blogs.Where(b => b.UserId == CurrentUserId || b.ProjectGroupId == ProjectGroups.First().Id).ToList();
            }

            else if (CurrentUser.IsInRole("Student"))
            {
                //ProjectGroups = await ProjectRepository.GetProjectGroupsForSpecificStudent(CurrentUserId);
                StudentGroups = await ProjectRepository.GetStudentGroupByStudentId(CurrentUserId);

                var projectGroups = !StudentGroups.Any() 
                    ? Enumerable.Empty<ProjectGroup>()
                    : ProjectGroups.Where(pg => pg.Id == StudentGroups.SingleOrDefault().ProjectGroupId).ToList(); 

                var blogs = await BlogRepository.GetAllBlogs();


                var ifExistBlogsForCurrentUserAndSpecificGroupThatBelongStudenToIt = blogs.Where(b => b.UserId == CurrentUserId && 
                b.ProjectGroupId == StudentGroups.Single().ProjectGroupId).ToList();


                Blogs = !ifExistBlogsForCurrentUserAndSpecificGroupThatBelongStudenToIt.Any() ?
                        Enumerable.Empty<Blog>() :
                        blogs.Where(b => b.UserId == CurrentUserId 
                        || b.ProjectGroupId == StudentGroups.Single().ProjectGroupId).ToList();
            }
        }

        protected async Task Delete(int blogId)
        {
            bool confirmed = await JSRuntime.InvokeAsync<bool>("confirm", "هل أنت متأكد من إكمال عملية الحذف؟");

            if (confirmed)
            {
                if (Blogs.Any(b => b.Id == blogId))
                {
                    await BlogRepository.RemoveBlog(blogId);
                    await JSRuntime.InvokeVoidAsync("alert", "تمت عملية الحذف بنجاح");
                    await OnInitializedAsync();
                }

                else
                {
                    await JSRuntime.InvokeVoidAsync("alert", "العنصر غير متوفر");
                    await OnInitializedAsync();
                }
            }
        }


    }
}
