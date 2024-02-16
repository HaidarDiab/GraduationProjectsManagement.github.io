using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProjectsManagement.Models
{
    public class ProjectGroup
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "الرجاء ملء الحقل")]
        public string Name { get; set; } = string.Empty;

        [ForeignKey("ProjectId")]
        public Project Project { get; } = new();


        [Required(ErrorMessage = "الرجاء ملء الحقل")]
        public int ProjectId { get; set; }


        [ForeignKey("SupervisorId")]
        public ApplicationUser Supervisor { get; } = new();

        [Required(ErrorMessage = "الرجاء ملء الحقل")]
        public string SupervisorId { get; set; } = string.Empty;

    }
}
