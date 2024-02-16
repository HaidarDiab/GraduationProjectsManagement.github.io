using GraduationProjectsManagement.Models;
using GraduationProjectsManagement.Repositories.Cotracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace GraduationProjectsManagement.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class AddProjectCategoryBase : ComponentBase
    {
        [Inject]
        public IProjectRepository ProjectRepository { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public IEnumerable<ProjectCategory> ProjectCategories { get; set; }

        public ProjectCategory Project = new ProjectCategory();

        [Parameter]
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;


        protected override async Task OnInitializedAsync()
        {
           
            ProjectCategories = await ProjectRepository.GetProjectCategories();

            if (Id != 0) 
            {
                Title = "تعديل تصنيف مشروع";
                Project.Name = ProjectCategories.First(p => p.Id == Id).Name;
            }
            else
            {
                Title = "إضافة تصنيف مشروع";
                Project = new();
            }
          
        }

        public async Task Save()
        {
            try
            {

                if (!ProjectCategories.Any(p => p.Id == Id)) 
                {
                    await ProjectRepository.AddProjectCategory(Project);

                    await JSRuntime.InvokeVoidAsync("alert", "تمت عملية الإضافة بنجاح");

                }
                else
                {
                    Project.Id = Id;

                    await ProjectRepository.UpdateProjectCategory(Project);

                    await JSRuntime.InvokeVoidAsync("alert", "تمت عملية التحديث بنجاح");

                }
            }
            catch (Exception)
            {
                await JSRuntime.InvokeVoidAsync("حدث خطأ أثناء عملية الإضافة حاول مجدداً");
            }

            NavigationManager.NavigateTo("/DisplayProjectCategories");

        }
        public void Cancel()
        {
            NavigationManager.NavigateTo("/DisplayProjectCategories");
        }
    }
}
