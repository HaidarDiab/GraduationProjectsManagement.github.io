using GraduationProjectsManagement.Models;
using Microsoft.AspNetCore.Identity;

namespace GraduationProjectsManagement.Repositories.Cotracts
{
    public interface IProjectRepository
    {
        //...................................................................................//
        Task <Project> GetProjectById(int id);
        Task<Project> GetProjectByProjectGroupId(int projectGroupId);
        Task<IEnumerable<Project>> GetAllProjects();
        Task <IEnumerable<Project>> GetSelectedProjects();

        Task<IEnumerable<Project>> GetProposalProjectByStudents();
        Task<IEnumerable<Project>> GetProposalProjectBySupervisors();
        Task<IEnumerable<Project>> GetPreviousProjects();

        Task<IEnumerable<ProjectType>> GetProjectTypes();
        Task<IEnumerable<ProjectCategory>> GetProjectCategories();

        Task<IEnumerable<Project>> GetProjectsByCategory(int categoryId);


        Task AddProject(Project project);
        Task UpdateProject(Project project);

        Task RemoveProject(int id);

        //...................................................................................//

        Task<bool> AddProjectType(ProjectType projectType);

        Task UpdateProjectType(ProjectType project);

        Task RemoveProjectType(int id);



        //...................................................................................//
        Task<bool> AddProjectCategory(ProjectCategory project);

        Task UpdateProjectCategory(ProjectCategory project);

        Task RemoveProjectCategory(int id);



      //.........................................................................................//
        Task<IEnumerable<ProjectGroup>> GetProjectGroups();
        Task<IEnumerable<ProjectGroup>> GetProjectGroupsForSpecificSupervisor(string supervisorId);
        Task<IEnumerable<ProjectGroup>> GetProjectGroupsForSpecificStudent(string currentUserId);
        Task<ProjectGroup> GetProjectGroupByGroupId(int groupId);
        Task<bool> AddProjectGroup(ProjectGroup group);
        Task UpdateProjectGroup(ProjectGroup group);
        Task RemoveProjectGroup(int groupId);


        //...................................................................................//


        //...................................................................................//
        Task<IEnumerable<StudentGroup>> GetStudentsGroups();
        Task<IEnumerable<StudentGroup>> GetStudentGroupByGroupId(int groupId);
        Task<IEnumerable<StudentGroup>> GetStudentGroupByStudentId(string studentId);

        Task<IEnumerable<StudentGroup>> GetStudentGroupsForSpecificSupervisorByJoinItWithProjectGrouppUsingId(string supervisorIdFromInterviewModel);
        Task AddStudentGroup(StudentGroup studentGroup);
        Task UpdateStudentGroup(StudentGroup studentGroup);
        Task RemoveStudentGroup(int studentGroupId);


        //...................................................................................//

        Task<IEnumerable<ProjectEvaluation>> GetProjectEvaluations();
        Task<ProjectEvaluation> GetSpecificProjectEvaluations(int projectGroupId);
        Task AddProjectEvaluation(ProjectEvaluation projectEvaluation);
        Task UpdateProjectEvaluation(ProjectEvaluation projectEvaluation);
        Task RemoveProjectEvaluation(int projectEvaluationId);

        string GetSpecificSupervisorForGroup(int projectEvaluationId);
        string GetSpecificProjectForGroupByProjectGroupId(int projectGroupId);
        string GetUsernameForStudentGroupByGroupId(int projectGroupId, string studentId);
        int GetProjectGroupIdByProjectEvaluationId(int projectEvaluationId);


        //...................................................................................//

        Task<IEnumerable<Interview>> GetInterviews();
        Task<Interview> GetInterviewByProjectGroupId(int projectGroupId);
        Task<Interview> GetInterviewByItsId(int interviewId);

        Task AddInterview(Interview interview);
        Task UpdateInterview(Interview interview);
        Task RemoveInterview(int interviewId);



        //.......................................................................................//

        Task RemoveCollections();


    }
}
