using GraduationProjectsManagement.Models;
using GraduationProjectsManagement.Repositories.Cotracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Notifications;
using System.Data;

namespace GraduationProjectsManagement.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class AddGroupBase : ComponentBase
    {
        [Inject]
        public IProjectRepository ProjectRepository { get; set; }


        [Inject]
        public IAccountRepository AccountRepository { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        public IEnumerable<ProjectGroup> ProjectGroups { get; set; } = Enumerable.Empty<ProjectGroup>();
        public IEnumerable<Project> Projects { get; set; } = Enumerable.Empty<Project>();
        public IEnumerable<IdentityUser> Supervisors { get; set; } = Enumerable.Empty<IdentityUser>();

        public ProjectGroup ProjectGroup = new();

        public IdentityUser Supervisor = new();

        public IdentityUser User = new();

        public Project Project = new();


        [Parameter]
        public int GroupId { get; set; }

        public string Title { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            Projects = await ProjectRepository.GetSelectedProjects();
            Supervisors = await AccountRepository.GetSupervisors();


            if (GroupId != 0)
            {
                Title = "تعديل مجموعة مشروع";

                ProjectGroup = await ProjectRepository.GetProjectGroupByGroupId(GroupId);
                ProjectGroup.Id = GroupId;

            }
            else
            {
                Title = "إضافة مجموعة مشروع";
                ProjectGroup = new();
            }
        }

        protected async Task Save()
        {

            try
            {
                ProjectGroups = await ProjectRepository.GetProjectGroups();


                if (GroupId == 0)
                {
                    if (!ProjectGroups.Any(p => p.Name == ProjectGroup.Name))
                    {
                        if (!ProjectGroups.Any(p => p.ProjectId == ProjectGroup.ProjectId))
                        {

                            await ProjectRepository.AddProjectGroup(ProjectGroup);

                            await JsRuntime.InvokeVoidAsync("alert", "تمت عملية إضافة المجموعة بنجاح");


                            NavigationManager.NavigateTo("/DisplayProjectsGroups");

                        }
                        else
                        {
                            await JsRuntime.InvokeVoidAsync("alert", "المشروع تم اختياره مسبقاً ضمن مجموعة");
                        }
                    }
                    else
                    {
                        await JsRuntime.InvokeVoidAsync("alert", "يوجد مجموعة تحمل نفس الاسم");

                    }
                }

                else if (GroupId !=0)
                {
                        ProjectGroup.Id = GroupId;


                        if (!ProjectGroups.Any(p => p.ProjectId == ProjectGroup.ProjectId))
                        {
                            await ProjectRepository.UpdateProjectGroup(ProjectGroup);

                            await JsRuntime.InvokeVoidAsync("alert", "تمت عملية التحديث بنجاح");
                            NavigationManager.NavigateTo("/DisplayProjectsGroups");
                        }

                        else
                        {
                            await JsRuntime.InvokeVoidAsync("alert", "المشروع تم اختياره مسبقاً ضمن مجموعة");
                        }

                }
            }
            catch (Exception)
            {
                await JsRuntime.InvokeVoidAsync("حدث خطأ أثناء عملية الإضافة حاول مجدداً");
            }
        }


        protected void Cancel()
        {
            NavigationManager.NavigateTo("/ControlPanel");
        }

    }
}
