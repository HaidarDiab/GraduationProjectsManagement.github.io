using GraduationProjectsManagement.Models;
using GraduationProjectsManagement.Repositories.Cotracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace GraduationProjectsManagement.Pages.Public
{
    public class DisplayMailsBase : ComponentBase
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
        public IEnumerable<SentMail> SentMails { get; set; } = new List<SentMail>();
        public IEnumerable<ReceivedMail> ReceivedMails { get; set; } = new List<ReceivedMail>();


        public IdentityUser User = new();

        public ClaimsPrincipal CurrentUser { get; set; }


        //maybe Student or Supervisor Or Admin Id
        public string? CurrentUserId { get; set; }
        public SentMail SentMail { get; set; } = new();

        public ReceivedMail ReceivedMail { get; set; } = new();

        public string? ButtonId { get; set; }

        protected async override Task OnInitializedAsync()
        {
            CurrentUser = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
            var identityUser = await _userManager.GetUserAsync(CurrentUser);
            CurrentUserId = identityUser.Id;

            SentMails = await MailRepository.GetSentMailsForCurrentLoggedUserByUserId(CurrentUserId);
            ReceivedMails = await MailRepository.GetReceivedMailsForCurrentLoggedUserByUserId(CurrentUserId);
        }


        protected async Task ViewSentMails()
        {
            ButtonId = "SentMails";
            SentMails.OrderByDescending(sm => sm.SentDate).ToList();

        }

        protected async Task ViewReceivedMails()
        {
            ButtonId = "ReceivedMails";

           ReceivedMails.OrderByDescending(rm => rm.ReceiveDate).ToList();

        }


        protected async Task Cancel()
        {
                NavigationManager.NavigateTo("/DisplaySpecificMailsType");
        }

    }
}
