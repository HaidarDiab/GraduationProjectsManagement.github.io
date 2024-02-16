using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GraduationProjectsManagement.Models
{
    public class ReceivedMail
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string From { get; set; } = string.Empty;

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Subject { get; set; } = string.Empty;

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Body { get; set; } = string.Empty;
        public DateTime ReceiveDate { get; set; }


        public bool ReceivingStatus { get; set; } = false;


        public MailStatus MailStatus { get; } = new();
        public int MailStatusId { get; set; }

        [ForeignKey("ReceivedUserUserId")]
        public virtual ApplicationUser ReceivedUser { get; } = new();

        public string ReceivedUserUserId { get; set; } = string.Empty;
    }
}
