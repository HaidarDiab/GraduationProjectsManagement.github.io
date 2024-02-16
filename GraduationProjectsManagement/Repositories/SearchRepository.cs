using Blazored.LocalStorage;
using GraduationProjectsManagement.Data;
using GraduationProjectsManagement.Repositories.Cotracts;
using Microsoft.EntityFrameworkCore;

namespace GraduationProjectsManagement.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        //private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private readonly ILocalStorageService _localStorageService;
        //private readonly ApplicationDbContext dbContext;
        private readonly ApplicationDbContext dbContext;

        public SearchRepository(ApplicationDbContext dbContextFactory, ILocalStorageService localStorageService)
        {
            //_dbContextFactory = dbContextFactory;
            _localStorageService = localStorageService;
            //dbContext = _dbContextFactory.CreateDbContext();
            dbContext = dbContextFactory;
        }


        public async Task<List<Result>> SearchAsync(string term)
        {
            // Validate and sanitize the term
            if (string.IsNullOrEmpty(term))
            {
                return new List<Result>();
            }

            // Perform a case-insensitive search for projects using LINQ
            var projectResults = await dbContext.Projects
              .Where(p => p.Title.Contains(term))
              .Select(p => new Result
              {
                  Url = $"/ProjectDetails/{p.Id}",
                  Title = p.Title,
                  Description = p.Description,
              })
              .ToListAsync();

            //// Perform a case-insensitive search for Projects Groups using LINQ
            var groupResults = await dbContext.ProjectGroups
              .Where(pg => pg.Name.Contains(term))
              .Select(pg => new Result
              {
                  Url = $"/DisplayProjectsGroups",
                  Title = pg.Name,
              })
              .ToListAsync();

            //// Perform a case-insensitive search for blogs using LINQ
            var blogResults = await dbContext.Blogs
              .Where(b => b.Title.Contains(term) ||
                          b.Title.Contains(term))
              .Select(b => new Result
              {
                  Url = $"/DisplayDiscussionBlog/{b.Id}",
                  Title = b.Title,
              })
              .ToListAsync();

            //// Combine the results from different queries using Union
            var results = projectResults.Union(groupResults).Union(blogResults).ToList();

            return results;
        }

    }
}
