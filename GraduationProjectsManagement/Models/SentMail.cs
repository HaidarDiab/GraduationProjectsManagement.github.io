using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace GraduationProjectsManagement.Models
{
    public class SentMail
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="هذا الحقل مطلوب")]
        [EmailAddress(ErrorMessage ="يجب إدخال إيميل صحيح")]
        public string To { get; set; } = string.Empty;

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Subject { get; set; } = string.Empty;

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Body { get; set; } = string.Empty;

        public DateTime SentDate { get; set; }
        public bool SendingStatus { get; set; } = false;

        public MailStatus MailStatus { get; } = new();
        public int MailStatusId { get; set; }

        [ForeignKey("SentUserId")]
        public virtual ApplicationUser SentUser { get; } = new();

        public string SentUserId { get; set; } = string.Empty;
    }
}
