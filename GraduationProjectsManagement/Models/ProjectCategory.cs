using System.ComponentModel.DataAnnotations;

namespace GraduationProjectsManagement.Models
{
    public class ProjectCategory
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "الرجاء ملء الحقل")]
        public string Name { get; set; } = string.Empty;
    }
}
