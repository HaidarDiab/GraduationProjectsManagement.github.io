using GraduationProjectsManagement.Models;
using GraduationProjectsManagement.Repositories.Cotracts;

using Microsoft.AspNetCore.Components;

namespace GraduationProjectsManagement.Pages.Public
{
    public class ProjectsByCategoryBase : ComponentBase
    {
        [Parameter]
        public int CategoryId { get; set; }

        [Inject]
        public IProjectRepository ProjectRepository { get; set; }

        public IEnumerable<Project> Projects { get; set; }
        public string CategoryName { get; set; }
        public string ErrorMessage { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                Projects = await GetProjectCollectionByCategoryId(CategoryId);
                
                if(Projects != null && Projects.Count() > 0)
                {
                    var project = Projects.FirstOrDefault(p => p.ProjectCategoryId == CategoryId);
                    
                    if (project != null)
                    {
                        CategoryName = project.ProjectCategory.Name;
                    }
                
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        private async Task<IEnumerable<Project>> GetProjectCollectionByCategoryId(int categoryId)
        {
            var projectCollection = await ProjectRepository.GetSelectedProjects();

            if(projectCollection != null)
            {
                return projectCollection.Where(p => p.ProjectCategoryId == categoryId);
            }
            else
            {
                return await ProjectRepository.GetProjectsByCategory(categoryId);
            }

        }

    }
}
