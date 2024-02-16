using GraduationProjectsManagement.Models;
using GraduationProjectsManagement.Repositories.Cotracts;
using Microsoft.AspNetCore.Components;

namespace GraduationProjectsManagement.Pages.Public
{
    public class DisplayMailDetailsBase : ComponentBase
    {
        [Inject]
        public IMailRepository MailRepository { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public int MailId { get; set; }

        [Parameter]
        public string ButtonId { get; set; }

      
        public IEnumerable<SentMail> SentMails { get; set; } = new List<SentMail>();
        public SentMail SentMail { get; set; } = new ();
        public IEnumerable<ReceivedMail> ReceivedMails { get; set; } = new List<ReceivedMail>();
        public ReceivedMail ReceivedMail { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            SentMails = await MailRepository.GetSentMails();
            ReceivedMails = await MailRepository.GetReceivedMails();
            SentMail = await MailRepository.GetSentMailsByMailId(MailId);
            ReceivedMail = await MailRepository.GetReceivedMailsByMailId(MailId);
            ButtonId = ButtonId;
        }

        protected async Task Cancel()
        {
            NavigationManager.NavigateTo("/DisplayMails");
        }

    }
}
