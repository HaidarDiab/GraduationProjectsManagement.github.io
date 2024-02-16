namespace GraduationProjectsManagement.Models
{
    public class ProjectEvaluation
    {
        public int Id { get; set; }
        public int Grade { get; set; }

        public string GradeLevelName { get; set; } = string.Empty;
        public ProjectGroup ProjectGroup { get; } = new();
        public int ProjectGroupId { get; set; }
    }
}
