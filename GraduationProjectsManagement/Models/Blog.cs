using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProjectsManagement.Models
{
    public class Blog
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="هذا الحقل مطلوب")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Question { get; set; } = string.Empty;
        public DateTime BlogDate { get; set; }
        public ProjectGroup ProjectGroup { get; } = new();
        public int ProjectGroupId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; } = new();
        public string UserId { get; set; } = string.Empty;

    }
}
