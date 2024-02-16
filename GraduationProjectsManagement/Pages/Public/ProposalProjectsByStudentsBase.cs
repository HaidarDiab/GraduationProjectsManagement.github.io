using GraduationProjectsManagement.Models;
using GraduationProjectsManagement.Repositories.Cotracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.ComponentModel.DataAnnotations;

namespace GraduationProjectsManagement.Pages.Public
{
    [Authorize]
    public class ProposalProjectsByStudentsBase : ComponentBase
    {
        [Inject]
        public IProjectRepository ProjectRepository { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }


        public IEnumerable<Project> Projects { get; set; } = Enumerable.Empty<Project>();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await ClearLocalStorage();
                         
                Projects = await ProjectRepository.GetProposalProjectByStudents();
            }
            catch (Exception)
            {

                //await JSRuntime.InvokeVoidAsync("alert", "حدث خطأ الرجاء المحاولة مجدداً");
            }
        }

        protected IOrderedEnumerable<IGrouping<int, Project>> GetGroupedProjectsByCategory()
        {
            return from project in Projects
                   group project by project.ProjectCategoryId into projByCatGroup
                   orderby projByCatGroup.Key
                   select projByCatGroup;
        }
        protected string GetCategoryName(IGrouping<int, Project> groupedProjects)
        {
            return groupedProjects.First(gp => gp.ProjectCategoryId == groupedProjects.Key).ProjectCategory.Name;
        }

        private async Task ClearLocalStorage()
        {
            await ProjectRepository.RemoveCollections();
        }
    }
}
