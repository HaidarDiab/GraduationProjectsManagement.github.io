using GraduationProjectsManagement.Models;

namespace GraduationProjectsManagement.Repositories.Cotracts
{
    public interface IBlogRepository
    {

        Task<IEnumerable<Blog>> GetAllBlogs();
        Task<IEnumerable<Blog>> GetAllBlogsForSpecificProjectGroupId(int projectGroupId);
        Task<Blog> GetBlogById(int blogId);
        Task<Blog> GetBlogByProjectGroupId(int projectGroupId);

        Task<IEnumerable<Blog>> GetAllBlogsForCurrentUserByUserId(string currentUserId);

        Task AddBlog(Blog blog);
        Task UpdateBlog(Blog blog);
        Task RemoveBlog(int blogId);


        //.........................Discussion Blogs..................................//

        Task<IEnumerable<DiscussionBlog>> GetAllDiscussionBogs();
        Task<DiscussionBlog> GetSpecificDiscussionBlogByDiscuutionBlogId(int discussionBlogId);
        Task<IEnumerable<DiscussionBlog>> GetAllDiscussionBogsForSpecificBlogIdByBlogId(int blogId);
        Task AddDiscussionBlog(DiscussionBlog discussionBlog);
        Task UpdateDiscussionBlog(DiscussionBlog discussionBlog);
        Task RemoveDiscussionBlog(int discussionBlogId);

    }
}
