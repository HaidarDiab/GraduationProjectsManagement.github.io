using GraduationProjectsManagement.Models;
using GraduationProjectsManagement.Repositories.Cotracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace GraduationProjectsManagement.Pages.Supervisor
{
    [Authorize(Roles ="Supervisor")]
    public class AddInterviewBase : ComponentBase
    {
        [Inject]
        public IProjectRepository ProjectRepository { get; set; }

        [Inject]
        public IAccountRepository AccountRepository { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public AuthenticationStateProvider _authenticationStateProvider { get; set; }

        [Inject]
        public UserManager<IdentityUser> _userManager { get; set; }



        public IEnumerable<Interview> Interviews { get; set; } = new List<Interview>();
        public IEnumerable<StudentGroup> StudentGroups { get; set; } = new List<StudentGroup>();

        public Interview Interview { get; set; } = new();

        [Parameter]
        public int InterviewId { get; set; }

        public ClaimsPrincipal CurrentUser { get; set; }

        public string? CurrentUserId { get; set; }
        public string? Title { get; set; }


        protected override async Task OnInitializedAsync()
        {

            CurrentUser = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
            var identityUser = await _userManager.GetUserAsync(CurrentUser);
            CurrentUserId = identityUser.Id;
            Interview = new();
            Interviews = await ProjectRepository.GetInterviews();
            StudentGroups = await ProjectRepository.GetStudentGroupsForSpecificSupervisorByJoinItWithProjectGrouppUsingId(CurrentUserId);


            if(InterviewId != 0)
            {
                Title = "تعديل موعد مقابلة";
                Interview.StudentGroupId = StudentGroups.FirstOrDefault(sg => sg.Id == Interviews.FirstOrDefault(i => i.Id == InterviewId).StudentGroupId).Id;
                Interview.InterviewDate = Interviews.FirstOrDefault(i => i.Id == InterviewId).InterviewDate;
            }

            else
            {
                Title = "إضافة موعد مقابلة";
                Interview = new();

            }
        }

        protected async Task Save()
        {
            
            if (InterviewId == 0)
            {
                if (!Interviews.Any(i => i.StudentGroupId == Interview.StudentGroupId))
                {
                    if (!Interviews.Any(i => i.InterviewDate == Interview.InterviewDate))
                    {
                        var newInterview = new Interview()
                        {
                            StudentGroupId = Interview.StudentGroupId,
                            InterviewDate = Interview.InterviewDate.Date,
                            InterviewTime = Interview.InterviewTime,
                            SupervisorId = CurrentUserId,
                        };
                        await ProjectRepository.AddInterview(newInterview);
                        await JSRuntime.InvokeVoidAsync("alert", "تمت عملية الإضافة بنجاح");
                        NavigationManager.NavigateTo("/DisplayInterviews");
                    }

                    else
                    {
                        await JSRuntime.InvokeVoidAsync("alert", "يوجد مقابلة بنفس الموعد");

                    }
                }
                else
                {
                    await JSRuntime.InvokeVoidAsync("alert", "يوجد مقابلة محددة مسبقاً لهذه المجموعة");

                }
            }

            else
            {
                Interview.Id = InterviewId;
                Interview.StudentGroupId = Interview.StudentGroupId;
                Interview.InterviewDate = Interview.InterviewDate.Date;
                Interview.InterviewTime = Interview.InterviewTime;
                Interview.SupervisorId = CurrentUserId;
                await ProjectRepository.UpdateInterview(Interview);
                await JSRuntime.InvokeVoidAsync("alert", "تمت عملية التحديث بنجاح");
                NavigationManager.NavigateTo("/DisplayInterviews");

            }

        }

        protected async Task Cancel()
        {
            NavigationManager.NavigateTo("/DisplayInterviews");

        }

    }

 
}
