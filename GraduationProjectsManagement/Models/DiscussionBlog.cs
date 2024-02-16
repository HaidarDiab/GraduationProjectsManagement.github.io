using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProjectsManagement.Models
{
    public class DiscussionBlog
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Answer { get; set; }

        public DateTime AnswerDate { get; set; }

        public Blog Blog { get; } = new();
        public int BlogId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; } = new();

        public string UserId { get; set; } = string.Empty;
    }
}
