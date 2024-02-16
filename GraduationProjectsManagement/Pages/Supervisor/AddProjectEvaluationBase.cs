using GraduationProjectsManagement.Models;
using GraduationProjectsManagement.Repositories.Cotracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace GraduationProjectsManagement.Pages.Supervisor
{
    public class AddProjectEvaluationBase : ComponentBase
    {

        [Inject]
        public IProjectRepository ProjectRepository { get; set; }


        [Inject]
        public IAccountRepository AccountRepository { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        [Inject]
        public AuthenticationStateProvider _authenticationStateProvider { get; set; }

        [Inject]
        public UserManager<IdentityUser> _userManager { get; set; }

        public IEnumerable<ProjectGroup> ProjectGroups { get; set; } = Enumerable.Empty<ProjectGroup>();
        public IEnumerable<StudentGroup> StudentGroups { get; set; } = Enumerable.Empty<StudentGroup>();

        public IEnumerable<ProjectEvaluation> ProjectEvaluations { get; set; } = Enumerable.Empty<ProjectEvaluation>();

        public ProjectEvaluation ProjectEvaluation = new ProjectEvaluation();
        public ProjectGroup ProjectGroup { get; set; } = new();

        public ClaimsPrincipal CurrentUser { get; set; } = new();

        public string? CurrentUserId { get; set; }

        [Parameter]
        public int ProjectEvaluationId { get; set; }

        public string Title { get; set; } = string.Empty;

        public int dropDownlistProjectGroupId {  get; set; }
        protected override async Task OnInitializedAsync()
        {

            CurrentUser = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
            var identityUser = await _userManager.GetUserAsync(CurrentUser);
            CurrentUserId = identityUser.Id;

            ProjectEvaluations = await ProjectRepository.GetProjectEvaluations();

            ProjectGroups = await ProjectRepository.GetProjectGroupsForSpecificSupervisor(CurrentUserId);

            StudentGroups = await ProjectRepository.GetStudentsGroups();


            if (ProjectEvaluationId != 0)
            {

                Title = "تعديل تقييم مشروع";


                ProjectEvaluation.ProjectGroupId = ProjectEvaluations.SingleOrDefault(pe => pe.Id == ProjectEvaluationId).ProjectGroupId;
                ProjectEvaluation.Grade = ProjectEvaluations.FirstOrDefault(pe => pe.Id == ProjectEvaluationId).Grade;
            }
            else
            {
                Title = "إضافة تقييم مشروع";
                ProjectEvaluation = new();
            }
        }

        protected async Task Save()
        {

            try
            {
                if (ProjectEvaluation.Grade < 60)
                {
                    ProjectEvaluation.GradeLevelName = "D";
                }
                else if (ProjectEvaluation.Grade >= 60 && ProjectEvaluation.Grade < 65)
                {
                    ProjectEvaluation.GradeLevelName = "C-";
                }

                else if (ProjectEvaluation.Grade >= 65 && ProjectEvaluation.Grade < 70)
                {
                    ProjectEvaluation.GradeLevelName = "C";
                }

                else if (ProjectEvaluation.Grade >= 70 && ProjectEvaluation.Grade < 75)
                {
                    ProjectEvaluation.GradeLevelName = "C+";
                }


                else if (ProjectEvaluation.Grade >= 75 && ProjectEvaluation.Grade < 80)
                {
                    ProjectEvaluation.GradeLevelName = "B-";
                }


                else if (ProjectEvaluation.Grade >= 80 && ProjectEvaluation.Grade < 85)
                {
                    ProjectEvaluation.GradeLevelName = "B";
                }

                else if (ProjectEvaluation.Grade >= 85 && ProjectEvaluation.Grade < 90)
                {
                    ProjectEvaluation.GradeLevelName = "B+";
                }

                else if (ProjectEvaluation.Grade >= 90 && ProjectEvaluation.Grade < 95)
                {
                    ProjectEvaluation.GradeLevelName = "A-";
                }

                else if (ProjectEvaluation.Grade >= 95 && ProjectEvaluation.Grade < 98)
                {
                    ProjectEvaluation.GradeLevelName = "A";
                }

                else
                {
                    ProjectEvaluation.GradeLevelName = "A+";

                }

                if (ProjectEvaluationId == 0)
                {
                    if (!ProjectEvaluations.Any(p => p.ProjectGroupId == ProjectEvaluation.ProjectGroupId))
                    {
                        if (StudentGroups.Any(sg => sg.ProjectGroupId == ProjectEvaluation.ProjectGroupId))
                        {

                        await ProjectRepository.AddProjectEvaluation(ProjectEvaluation);

                        await JsRuntime.InvokeVoidAsync("alert", "تمت عملية إضافة التقييم بنجاح");

                        }

                        else await JsRuntime.InvokeVoidAsync("alert", "لا يمكنك تقييم المجموعة لأنها فارغة  لا يوجد فيها مشتركين");

                        NavigationManager.NavigateTo("/ProjectsEvaluations");
                    }
                    else
                    {
                        await JsRuntime.InvokeVoidAsync("alert", "يوجد تقييم سابق");

                    }
                }

                else if (ProjectEvaluationId != 0)
                {
                    ProjectEvaluation.Id = ProjectEvaluationId;

                    await ProjectRepository.UpdateProjectEvaluation(ProjectEvaluation);

                    await JsRuntime.InvokeVoidAsync("alert", "تمت عملية التحديث بنجاح");
                    NavigationManager.NavigateTo("/ProjectsEvaluations");
                }
            }
            catch (Exception)
            {
                await JsRuntime.InvokeVoidAsync("حدث خطأ أثناء عملية الإضافة حاول مجدداً");
            }
        }


        protected void Cancel()
        {
            NavigationManager.NavigateTo("/ProjectsEvaluations");
        }
    }
}
