using GraduationProjectsManagement.Models;
using GraduationProjectsManagement.Repositories.Cotracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace GraduationProjectsManagement.Pages.Public
{
    public class DisplayDiscussionBlogBase : ComponentBase
    {
        [Inject]
        public IBlogRepository BlogRepository { get; set; }

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


        public IEnumerable<DiscussionBlog> DiscussionsBlogs { get; set; }= new List<DiscussionBlog>();
        public IEnumerable<Blog> Blogs { get; set; }= new List<Blog>();
        public IEnumerable<IdentityUser> Users { get; set; }= new List<IdentityUser>();

       
        public DiscussionBlog DiscussionBlog { get; set; } = new();
        public Blog Blog { get; set; } = new();

        public IdentityUser User = new();

        [Parameter]
        public int blogId { get; set; }

        public int DiscussionBlogId { get; set; }

        public ClaimsPrincipal CurrentUser { get; set; }


        //maybe Student or Supervisor Or Admin Id

        public string? CurrentUserId { get; set; }

        public string? ButtonTitle { get; set; }


        protected override async Task OnInitializedAsync()
        {

            CurrentUser = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
            var identityUser = await _userManager.GetUserAsync(CurrentUser);
            CurrentUserId = identityUser.Id;

            DiscussionsBlogs = await BlogRepository.GetAllDiscussionBogsForSpecificBlogIdByBlogId(blogId);

            Blogs = await BlogRepository.GetAllBlogs();

            Blog = await BlogRepository.GetBlogById(blogId);

            Users = await AccountRepository.GetAllUsers();

            DiscussionBlog.Answer = null;
            ButtonTitle = "رد";

        }
        protected async Task Save()
        {
            if (DiscussionBlogId == 0)
            {
                DiscussionBlog.BlogId = blogId;
                DiscussionBlog.UserId = CurrentUserId;
                await BlogRepository.AddDiscussionBlog(DiscussionBlog);

                await OnInitializedAsync();
            }

            else
            {
                DiscussionBlog.Id = DiscussionBlogId;
                DiscussionBlog.BlogId = blogId;
                DiscussionBlog.UserId = CurrentUserId;
                await BlogRepository.UpdateDiscussionBlog(DiscussionBlog);
                await JSRuntime.InvokeVoidAsync("alert", "تمت عملية التحديث بنجاح");

                await OnInitializedAsync();
            }
        }


        protected async Task Update(int discussionBlogId)
        {
            await OnInitializedAsync();

            await JSRuntime.InvokeVoidAsync("alert", "أصبح المحتوى المراد تعديله موجود في الصندوق النصي في الأسفل، عدّل ما تريد واضغط على تعديل الرد");

            var blogDiscussion = await BlogRepository.GetSpecificDiscussionBlogByDiscuutionBlogId(discussionBlogId);
            ButtonTitle = "تعديل الرد";

                DiscussionBlog.Answer = blogDiscussion.Answer;
            DiscussionBlogId = DiscussionBlog.Id = blogDiscussion.Id;
        }

        protected async Task Delete(int discussionBlogId)
        {
            bool confirmed = await JSRuntime.InvokeAsync<bool>("confirm", "هل أنت متأكد من إكمال عملية الحذف؟");

            var discussionBlog = await BlogRepository.GetSpecificDiscussionBlogByDiscuutionBlogId(discussionBlogId);

            if (confirmed)
            {
                if (discussionBlog != null)
                {
                    await BlogRepository.RemoveDiscussionBlog(discussionBlogId);
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

        public void Cancel()
        {
            NavigationManager.NavigateTo("/DisplayBlogs");
        }

    }
}
