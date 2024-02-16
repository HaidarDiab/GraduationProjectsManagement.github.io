using GraduationProjectsManagement.Models;
using GraduationProjectsManagement.Repositories.Cotracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace GraduationProjectsManagement.Pages.Public
{
    [Authorize]
    public class SendEmailBase : ComponentBase
    {

        [Inject]
        public IMailRepository MailRepository { get; set; }

        [Inject]
        public IAccountRepository AccountRepository { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IJSRuntime jSRuntime { get; set; }

        [Inject]
        public AuthenticationStateProvider _authenticationStateProvider { get; set; } 

        [Inject]
        public UserManager<IdentityUser> _userManager { get; set; }

        public IEnumerable<IdentityUser> Users { get; set; } = new List<IdentityUser>();

        public IdentityUser User { get; set; } = new();

        public ClaimsPrincipal CurrentUser { get; set; }


        //maybe Student or Supervisor Or Admin Id
        public string? CurrentUserId { get; set; }
        public SentMail SentMail { get; set; } = new();

        public ReceivedMail ReceivedMail { get; set; } = new();

        protected async override Task OnInitializedAsync()
        {
            CurrentUser = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
            var identityUser = await _userManager.GetUserAsync(CurrentUser);
            CurrentUserId = identityUser.Id;

            Users = await AccountRepository.GetAllUsers();
        }



        protected async Task SendEmail()
        {
            SentMail.SentUserId = CurrentUserId;
            await MailRepository.SendEmailAsync(SentMail);

            await jSRuntime.InvokeVoidAsync("alert", "تم إرسال البريد بنجاح");
        }




        public void Cancel()
        {
            NavigationManager.NavigateTo("/DisplayMails");
        }
    }
}
