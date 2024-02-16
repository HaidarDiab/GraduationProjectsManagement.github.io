using GraduationProjectsManagement.Models;
using Microsoft.AspNetCore.Components;

namespace GraduationProjectsManagement.Pages
{
    public class DisplayProjectsBase : ComponentBase
    {
        [Parameter]
        public IEnumerable<Project> Projects { get; set; }  = Enumerable.Empty<Project>(); 
        
    }
}
