using GraduationProjectsManagement.Data.Migrations;
using GraduationProjectsManagement.Repositories;
using GraduationProjectsManagement.Repositories.Cotracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GraduationProjectsManagement.Shared
{
    public class SearchBase : ComponentBase
    {
        [Inject]
        public ISearchRepository SearchRepository { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }


        [Inject]
        public State State { get; set; }

        public string searchTerm { get; set; }


        protected async override Task OnInitializedAsync()
        {
            searchTerm = null;
        }

        protected async Task Search(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    // Call the search service with the term
                    State.Results = await SearchRepository.SearchAsync(searchTerm);

                    await OnInitializedAsync();
                    NavigationManager.NavigateTo("/SearchResults");
                }
            }
        }
    }
}

