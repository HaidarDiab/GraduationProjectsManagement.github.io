using Blazored.LocalStorage;
using GraduationProjectsManagement.Data;
using GraduationProjectsManagement.Models;
using GraduationProjectsManagement.Repositories.Cotracts;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;

namespace GraduationProjectsManagement.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private readonly ILocalStorageService _localStorageService;
        private readonly ApplicationDbContext dbContext;

        public BlogRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory, ILocalStorageService localStorageService)
        {
            _dbContextFactory = dbContextFactory; 
            _localStorageService = localStorageService;
            dbContext = _dbContextFactory.CreateDbContext();
        }


        public async Task<IEnumerable<Blog>> GetAllBlogs()
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            return await dbContext.Blogs.ToListAsync();
        }
        public async Task<IEnumerable<Blog>> GetAllBlogsForSpecificProjectGroupId(int projectGroupId)
        {
             return await dbContext.Blogs.Where(b => b.ProjectGroupId == projectGroupId).ToListAsync();
        }

        public async Task<Blog> GetBlogById(int blogId)
        {
            return await dbContext.Blogs.SingleOrDefaultAsync(b => b.Id == blogId) as Blog;
        }

        public async Task<Blog> GetBlogByProjectGroupId(int projectGroupId)
        {
            return await dbContext.Blogs.SingleOrDefaultAsync(b => b.ProjectGroupId == projectGroupId) as Blog;

        }

        
        public async Task<IEnumerable<Blog>> GetAllBlogsForCurrentUserByUserId(string currentUserId)
        {
            return await dbContext.Blogs.Where(b => b.UserId == currentUserId).ToListAsync();
        }


        public async Task AddBlog(Blog blog)
        {
            var newBlog = new Blog()
            {
                Title = blog.Title,
                Question = blog.Question,
                BlogDate = DateTime.UtcNow,
                ProjectGroupId = blog.ProjectGroupId,
                UserId = blog.UserId,
            };

            await dbContext.Blogs.AddAsync(newBlog);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateBlog(Blog blog)
        {
           var blogInDb = await dbContext.Blogs.SingleOrDefaultAsync(b => b.Id == blog.Id);
            blogInDb.Title = blog.Title;
            blogInDb.Question = blog.Question;
            blogInDb.BlogDate = DateTime.UtcNow;
            blogInDb.ProjectGroupId = blog.ProjectGroupId;
            blog.UserId = blog.UserId;

            await dbContext.SaveChangesAsync();
        }

        public async Task RemoveBlog(int blogId)
        {
            var blogInDb = await dbContext.Blogs.SingleOrDefaultAsync(b => b.Id == blogId);

            dbContext.Blogs.Remove(blogInDb);
            await dbContext.SaveChangesAsync();
        }



        //.................................................Discussion blog...............................//

        public async Task<IEnumerable<DiscussionBlog>> GetAllDiscussionBogs()
        {
            return await dbContext.DiscussionBlogs.ToListAsync();
        }

        public async Task<DiscussionBlog> GetSpecificDiscussionBlogByDiscuutionBlogId(int discussionBlogId)
        {
            return await dbContext.DiscussionBlogs.FirstOrDefaultAsync(db => db.Id == discussionBlogId);
        }


        public async Task<IEnumerable<DiscussionBlog>> GetAllDiscussionBogsForSpecificBlogIdByBlogId(int blogId)
        {
            return await dbContext.DiscussionBlogs.Where(db => db.BlogId == blogId).ToListAsync();
        }


    

        public async Task AddDiscussionBlog(DiscussionBlog discussionBlog)
        {
            var newDiscussion = new DiscussionBlog()
            {
                Answer = discussionBlog.Answer,
                AnswerDate = DateTime.UtcNow,
                BlogId = discussionBlog.BlogId,
                UserId = discussionBlog.UserId,
            };

            await dbContext.DiscussionBlogs.AddAsync(newDiscussion);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateDiscussionBlog(DiscussionBlog discussionBlog)
        {
            var discussionInDb = await dbContext.DiscussionBlogs.SingleOrDefaultAsync(db => db.Id == discussionBlog.Id && db.BlogId == discussionBlog.BlogId);

            discussionInDb.Answer = discussionBlog.Answer;
            discussionInDb.AnswerDate = DateTime.UtcNow;
            discussionInDb.BlogId = discussionBlog.BlogId;
            discussionInDb.UserId = discussionBlog.UserId;

            await dbContext.SaveChangesAsync();

        }

        public async Task RemoveDiscussionBlog(int discussionBlogId)
        {
            var discussionInDb = await dbContext.DiscussionBlogs.SingleOrDefaultAsync(db => db.Id == discussionBlogId);

            dbContext.DiscussionBlogs.Remove(discussionInDb);

            await dbContext.SaveChangesAsync();
        }

    }
}
