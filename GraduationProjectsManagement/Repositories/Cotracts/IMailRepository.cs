using GraduationProjectsManagement.Models;
using System.Collections.Generic;

namespace GraduationProjectsManagement.Repositories.Cotracts
{
    public interface IMailRepository
    {

   
        Task<SentMail> GetSentMailsByMailId(int mailId);
        Task<IEnumerable<SentMail>> GetSentMails();
        Task<ReceivedMail> GetReceivedMailsByMailId(int mailId);
        Task<IEnumerable<ReceivedMail>> GetReceivedMails();
        Task<IEnumerable<SentMail>> GetSentMailsForCurrentLoggedUserByUserId(string currentUserId);
        Task<IEnumerable<ReceivedMail>> GetReceivedMailsForCurrentLoggedUserByUserId(string currentUserId);
        Task SendEmailAsync(SentMail sentMail);
        Task RemoveMail(int mailId);
    }

}
