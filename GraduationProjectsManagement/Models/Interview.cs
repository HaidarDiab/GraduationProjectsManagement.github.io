using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProjectsManagement.Models
{
    public class Interview
    {
        public int Id { get; set; }

        public DateTime InterviewDate { get; set; }
        public DateTime InterviewTime { get; set; }
        public StudentGroup StudentGroup { get; } = new();

        public int StudentGroupId { get; set; }

        [ForeignKey("SupervisorId")]
        public virtual ApplicationUser Supervisor { get; } = new();

        public string SupervisorId { get; set; } = string.Empty;
    }
}
