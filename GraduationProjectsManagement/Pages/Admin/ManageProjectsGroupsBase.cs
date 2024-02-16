using Microsoft.AspNetCore.Components;

namespace GraduationProjectsManagement.Pages.Admin
{
    public class ManageProjectsGroupsBase : ComponentBase
    {
        [Parameter]
        public int GroupId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            GroupId = 0;
        }
    }
}
