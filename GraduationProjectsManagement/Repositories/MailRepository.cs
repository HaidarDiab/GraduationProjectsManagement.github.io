using System.Net.Mail;
using System.Net;
using GraduationProjectsManagement.Repositories.Cotracts;
using GraduationProjectsManagement.Models;
using Microsoft.Extensions.Options;
using Blazored.LocalStorage;
using GraduationProjectsManagement.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

namespace GraduationProjectsManagement.Repositories
{

        public class MailRepository : IMailRepository
        {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private readonly ILocalStorageService _localStorageService;
        private readonly ApplicationDbContext dbContext;

        private const string key = "ProjectCollection";
        public MailRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory, ILocalStorageService localStorageService)
        {
            _dbContextFactory = dbContextFactory;
            _localStorageService = localStorageService;
            dbContext = _dbContextFactory.CreateDbContext();
        }

            public async Task SendEmailAsync(SentMail sentMail)
        {
            var newSentMail = new SentMail()
            {
                To = sentMail.To,
                Subject = sentMail.Subject,
                Body = sentMail.Body,
                SentDate = DateTime.UtcNow,
                SendingStatus = true,
                MailStatusId = 1,
                SentUserId = sentMail.SentUserId,
            };

            await dbContext.SentMails.AddAsync(newSentMail);
            await dbContext.SaveChangesAsync();


            //store id from users that equals id from sentmailtable, id for user who sent email
            var userEmailWhoSentMail = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == sentMail.SentUserId);

            //store receiveduser who have email that equals email sent to in sentmail model
            var receiveUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == sentMail.To);

            if (receiveUser == null)
            {
                //show error
            }

            else
            {

            var newReceivedMail = new ReceivedMail()
            {
                From = userEmailWhoSentMail.Email,
                Subject = sentMail.Subject,
                Body = sentMail.Body,
                ReceiveDate = DateTime.UtcNow,
                ReceivingStatus = true,
                MailStatusId = 2,
                ReceivedUserUserId = receiveUser.Id,
            };

            await dbContext.ReceivedMails.AddAsync(newReceivedMail);
            await dbContext.SaveChangesAsync();
            }
        }

        async Task IMailRepository.RemoveMail(int mailId)
        {
            var sentMail = await dbContext.SentMails.FirstOrDefaultAsync(s => s.Id == mailId);

            if (sentMail == null) 
            {
                var receivedMail = await dbContext.ReceivedMails.FirstOrDefaultAsync(rm => rm.Id == mailId);

                dbContext.ReceivedMails.Remove(receivedMail);

                await dbContext.SaveChangesAsync();
            }

            else
            {
                dbContext.SentMails.Remove(sentMail);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<SentMail>> GetSentMailsForCurrentLoggedUserByUserId(string currentUserId)
        {
            //the second condition represent the number 1 that consider the id for Sent Status from MailStatus Table
            return await dbContext.SentMails.Where(sm => sm.SentUserId == currentUserId && sm.MailStatusId == 1).ToListAsync();
        }

        public async Task<IEnumerable<ReceivedMail>> GetReceivedMailsForCurrentLoggedUserByUserId(string currentUserId)
        {
            //the second condition represent the number 1 that consider the id for Sent Status from MailStatus Table
            return await dbContext.ReceivedMails.Where(rm => rm.ReceivedUserUserId == currentUserId && rm.MailStatusId == 2).ToListAsync();
        }

        public async Task<IEnumerable<SentMail>> GetSentMails()
        {
            return await dbContext.SentMails.ToListAsync();
        }



        public async Task<IEnumerable<ReceivedMail>> GetReceivedMails()
        {
            return await dbContext.ReceivedMails.ToListAsync();
        }



        public async Task<SentMail> GetSentMailsByMailId(int mailId)
        {
            return await dbContext.SentMails.FirstOrDefaultAsync(m => m.Id == mailId);
        }

        public async Task<ReceivedMail> GetReceivedMailsByMailId(int mailId)
        {
            return await dbContext.ReceivedMails.FirstOrDefaultAsync(m => m.Id == mailId);

        }
    }
}

