using GraduationProjectsManagement.Models;
using GraduationProjectsManagement.Repositories.Cotracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace GraduationProjectsManagement.Pages.Admin
{
    [Authorize(Roles = "Admin")]

    public class AddProjectTypeBase : ComponentBase
    {
        [Inject]
        public IProjectRepository ProjectRepository { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IJSRuntime JsRuntime { get; set; }
        public IEnumerable<ProjectType> ProjectTypes { get; set; }

        public ProjectType Project = new ProjectType();

        public string? Message { get; set; }

        public string Title { get; set; } = string.Empty;

        [Parameter]
        public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ProjectTypes = await ProjectRepository.GetProjectTypes();

            if (Id != 0)
            {
                Title = "تعديل فئة مشروع";
                Project.Type = ProjectTypes.First(p => p.Id == Id).Type;
            }
            else
            {
                Title = "إضافة فئة مشروع";
                Project = new();
            }


        }

        public async Task Save()
        {
            try
            {

                if (!ProjectTypes.Any(p => p.Id == Id))
                {
                   await ProjectRepository.AddProjectType(Project);

                    await JsRuntime.InvokeVoidAsync("alert", "تمت العملية الإضافة بنجاح");

                }
                else
                {
                    Project.Id = Id;

                    await ProjectRepository.UpdateProjectType(Project);

                    await JsRuntime.InvokeVoidAsync("alert", "تمت عملية التحديث بنجاح");
                }
            }
            catch (Exception)
            {
                Message = "حدث خطأ حاول مجدداً";

            }
            NavigationManager.NavigateTo("/DisplayProjectTypes");


        }
        public void Cancel()
        {
            NavigationManager.NavigateTo("/DisplayProjectTypes");
        }

        private async Task ClearLocalStorage()
        {
            await ProjectRepository.RemoveCollections();
        }
    }
}
