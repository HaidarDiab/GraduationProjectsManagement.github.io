using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProjectsManagement.Models
{
    public class StudentGroup
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public ProjectGroup ProjectGroup { get; } = new();

        [Required(ErrorMessage = "الرجاء ملء الحقل")]
        public int ProjectGroupId { get; set; }

        [ForeignKey("StudentId")]
        public ApplicationUser Student { get; } = new();

        [Required(ErrorMessage = "الرجاء ملء الحقل")]
        public string StudentId { get; set; }
    }
}
