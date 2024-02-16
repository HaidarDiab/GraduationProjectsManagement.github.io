using GraduationProjectsManagement.Models;
using GraduationProjectsManagement.Repositories.Cotracts;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace GraduationProjectsManagement.Pages.Public
{
    public class DisplayProjectsTypesBase : ComponentBase
    {
        
       [Inject]
        public IProjectRepository ProjectRepository { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        protected IEnumerable<ProjectType> ProjectTypes { get; set; } = new List<ProjectType>();


        protected override async Task OnInitializedAsync()
        {
            ProjectTypes = await ProjectRepository.GetProjectTypes();
        }


        protected async Task Delete(int id)
        {
            bool confirmed = await JsRuntime.InvokeAsync<bool>("confirm", "هل أنت متأكد من إكمال عملية الحذف؟");
            if (confirmed)
            {
            await ProjectRepository.RemoveProjectType(id);
            }
            await JsRuntime.InvokeVoidAsync("alert", "تمت عملية الحذف بنجاح");

            NavigationManager.NavigateTo("/DisplayProjectTypes");
        }


    }
}
