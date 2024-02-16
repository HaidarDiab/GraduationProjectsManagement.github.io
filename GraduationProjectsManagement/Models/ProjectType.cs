using System.ComponentModel.DataAnnotations;


namespace GraduationProjectsManagement.Models
{
    public class ProjectType
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="الرجاء ملء الحقل")]
        public string Type { get; set; } = string.Empty;
    }
}
