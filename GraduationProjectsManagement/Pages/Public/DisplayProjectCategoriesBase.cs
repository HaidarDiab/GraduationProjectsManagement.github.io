using GraduationProjectsManagement.Models;
using GraduationProjectsManagement.Repositories.Cotracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace GraduationProjectsManagement.Pages.Public
{
    [Authorize]
    public class DisplayProjectCategoriesBase : ComponentBase
    {
        [Inject]
        public IProjectRepository ProjectRepository { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        public IEnumerable<ProjectCategory> ProjectCategories { get; set; } = new List<ProjectCategory>();

        protected override async Task OnInitializedAsync()
        {
            ProjectCategories = await ProjectRepository.GetProjectCategories();
        }

        protected async Task Delete(int CategoryId)
        {
            bool confirmed = await JsRuntime.InvokeAsync<bool>("confirm", "هل أنت متأكد من إكمال عملية الحذف؟");

            if (confirmed)
            {
                await ProjectRepository.RemoveProjectCategory(CategoryId);
            }

            await JsRuntime.InvokeVoidAsync("alert", "تمت عملية الحذف بنجاح");

            NavigationManager.NavigateTo("/DisplayProjectCategories");
        }

    }
}
