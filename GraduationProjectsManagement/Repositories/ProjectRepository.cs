using GraduationProjectsManagement.Repositories.Cotracts;
using GraduationProjectsManagement.Models;
using Microsoft.EntityFrameworkCore;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Identity;
using GraduationProjectsManagement.Data;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore.Internal;
using GraduationProjectsManagement.Pages.Supervisor;

namespace GraduationProjectsManagement.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
       
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private readonly ILocalStorageService _localStorageService;
        private readonly ApplicationDbContext dbContext;


        private const string key = "ProjectCollection";
        public ProjectRepository( IDbContextFactory<ApplicationDbContext> dbContextFactory, ILocalStorageService localStorageService)
        {
            _dbContextFactory = dbContextFactory; 
            _localStorageService = localStorageService;
            dbContext = _dbContextFactory.CreateDbContext();
        }

        //....................................//Projects............................................//


        public async Task<Project> GetProjectById(int id)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            var project = await dbContext.Projects.FirstOrDefaultAsync(p => p.Id == id);
            return project;
        }

        public async Task <Project> GetProjectByProjectGroupId(int projectGroupId)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            var projectsGroup = await dbContext.ProjectGroups.FirstOrDefaultAsync(pg => pg.Id == projectGroupId);

            return await dbContext.Projects.FirstOrDefaultAsync(p => p.Id == projectsGroup.ProjectId);
        }


        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            if (!dbContext.Projects.Any()) 
                return Enumerable.Empty<Project>();

            return await dbContext.Projects.ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetSelectedProjects()
        {
            //using var dbContext = _dbContextFactory.CreateDbContext();

            return await dbContext.Projects.Where(p => p.IsSelected == true).ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetProposalProjectByStudents()
        {
            //using var dbContext = _dbContextFactory.CreateDbContext();

            var projects = from Project in dbContext.Projects
                           join userRole in dbContext.UserRoles on Project.UserId equals userRole.UserId
                           join role in dbContext.Roles on userRole.RoleId equals role.Id
                           where role.Name == "Student"
                           select Project;

            return projects;
        }

        public async Task<IEnumerable<Project>> GetProposalProjectBySupervisors()
        {
            //using var dbContext = _dbContextFactory.CreateDbContext();

            var projects = from Project in dbContext.Projects
                           join userRole in dbContext.UserRoles on Project.UserId equals userRole.UserId
                           join role in dbContext.Roles on userRole.RoleId equals role.Id
                           where role.Name == "Supervisor" || role.Name == "Admin"
                           select Project;

            return projects;
        }

        public async Task<IEnumerable<Project>> GetPreviousProjects()
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            var date = DateTime.Parse("2023/4/1");

            return await dbContext.Projects.Where(p => p.AddedDate.Value.Date < date.Date).ToListAsync();
        }

        //............................................................................................//



        public async Task<IEnumerable<ProjectType>> GetProjectTypes()
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            if (!dbContext.ProjectTypes.Any())
                return Enumerable.Empty<ProjectType>();

            return await dbContext.ProjectTypes.ToListAsync();
        }

        public async Task<IEnumerable<ProjectCategory>> GetProjectCategories()
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            if (!dbContext.ProjectCategories.Any()) 
                return Enumerable.Empty<ProjectCategory>();

            return await dbContext.ProjectCategories.ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetProjectsByCategory(int categoryId)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            return await dbContext.Projects.OrderByDescending(p => p.ProjectCategoryId == categoryId).ToListAsync();
        }

        public async Task AddProject(Project project)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            try
            {

                var newProject = new Project()
                {
                    Title = project.Title,
                    Description = project.Description,
                    AddedDate = DateTime.Now,
                    Image = "",
                    ImageFile = project.ImageFile,
                    ProjectCategoryId = project.ProjectCategoryId,
                    ProjectTypeId = dbContext.ProjectTypes.First(p => p.Type == "مقترح مشروع").Id,
                    UserId = project.UserId,
                };


                await dbContext.Projects.AddAsync(newProject);

                await dbContext.SaveChangesAsync();



            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task UpdateProject(Project project)
        {
            //using var dbContext = _dbContextFactory.CreateDbContext();

            var projectInDb = dbContext.Projects.First(p => p.Id == project.Id);

            projectInDb.Title = project.Title;
            projectInDb.Description = project.Description;
            projectInDb.AddedDate = DateTime.Now;
            projectInDb.ImageFile = project.ImageFile;
            projectInDb.Image = project.Image;
            projectInDb.ProjectCategoryId = project.ProjectCategoryId;
            projectInDb.ProjectTypeId = project.ProjectTypeId;
            projectInDb.UserId = project.UserId;
            projectInDb.IsSelected = project.IsSelected;

            await dbContext.SaveChangesAsync();
        }

        public async Task RemoveProject(int id)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            var item = dbContext.Projects.Single(p => p.Id == id);

            dbContext.Projects.Remove(item);
            await dbContext.SaveChangesAsync();
        }
        //....................................//End Of Projects............................................//





        //....................................//Project Type............................................//

        public async Task<bool> AddProjectType(ProjectType project)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();


            var newProject = new ProjectType()
            {
                Id = project.Id,
                Type = project.Type,
            };

            await dbContext.ProjectTypes.AddAsync(newProject);

            await dbContext.SaveChangesAsync();
            return true;

        }

        public async Task UpdateProjectType(ProjectType project)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            var projectInDb = dbContext.ProjectTypes.Single(p => p.Id == project.Id);
            projectInDb.Type = project.Type;
            await dbContext.SaveChangesAsync();
        }



        public async Task RemoveProjectType(int id)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            var project = await dbContext.ProjectTypes.FindAsync(id);

            dbContext.ProjectTypes.Remove(project);

            await dbContext.SaveChangesAsync();

        }

        //....................................//End Of Project Type............................................//




        //....................................//Project Category............................................//


        public async Task<bool> AddProjectCategory(ProjectCategory project)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            var newProject = new ProjectCategory()
            {
                Id = project.Id,
                Name = project.Name,
            };

            await dbContext.ProjectCategories.AddAsync(newProject);

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task UpdateProjectCategory(ProjectCategory project)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            var projectInDb = dbContext.ProjectCategories.Single(p => p.Id == project.Id);

            projectInDb.Name = project.Name;

            await dbContext.SaveChangesAsync();
        }

        public async Task RemoveProjectCategory(int id)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            var project = dbContext.ProjectCategories.Find(id);

            dbContext.ProjectCategories.Remove(project);
            await dbContext.SaveChangesAsync();
        }

        //....................................//End Of ProjectCategory............................................//


        public async Task RemoveCollections()
        {
            await _localStorageService.RemoveItemAsync(key);

        }






        //....................................//Groups............................................//
        public async Task<IEnumerable<ProjectGroup>> GetProjectGroups()
        {
            //using var dbContext = _dbContextFactory.CreateDbContext();
            if (!dbContext.ProjectGroups.Any()) 
                return Enumerable.Empty<ProjectGroup>();

            return await dbContext.ProjectGroups.ToListAsync();
        }

        public async Task<IEnumerable<ProjectGroup>> GetProjectGroupsForSpecificSupervisor(string supervisorId)
        {
            //using var dbContext = _dbContextFactory.CreateDbContext();

            return await dbContext.ProjectGroups.Where(pg => pg.SupervisorId == supervisorId).ToListAsync();
        }

        public async Task<IEnumerable<ProjectGroup>> GetProjectGroupsForSpecificStudent(string studentId)
        {
            return (from projectGroup in dbContext.ProjectGroups
                     join studentGroup in dbContext.StudentGroups on projectGroup.Id equals studentGroup.ProjectGroupId
                     join student in dbContext.Users on studentGroup.StudentId equals student.Id
                     where student.Id == studentId
                    select projectGroup);
        }

        public async Task<ProjectGroup> GetProjectGroupByGroupId(int groupId)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            return await dbContext.ProjectGroups.FirstOrDefaultAsync(pg => pg.Id == groupId);
        }


        public async Task<bool> AddProjectGroup(ProjectGroup group)
        {
            //using var context = _dbContextFactory.CreateDbContext();
            var newProjectGroup = new ProjectGroup()
            {
                Name = group.Name,
                ProjectId = group.ProjectId,
                SupervisorId = group.SupervisorId,
            };

            dbContext.ProjectGroups.Add(newProjectGroup);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task UpdateProjectGroup(ProjectGroup group)
        {
            //using var dbContext = _dbContextFactory.CreateDbContext();

            var groupInDb = await dbContext.ProjectGroups.FirstOrDefaultAsync(pg => pg.Id == group.Id);

            groupInDb.Name = group.Name;
            groupInDb.ProjectId = group.ProjectId;
            groupInDb.SupervisorId = group.SupervisorId;

            await dbContext.SaveChangesAsync();
        }

        public async Task RemoveProjectGroup(int groupId)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            var groupInDb = await dbContext.ProjectGroups.FirstOrDefaultAsync(pg => pg.Id == groupId);

            dbContext.ProjectGroups.Remove(groupInDb);
            await dbContext.SaveChangesAsync();
        }


        //....................................//End Of Groups............................................//




        //....................................//Students Groups............................................//

        public async Task<IEnumerable<StudentGroup>> GetStudentsGroups()
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            if (!dbContext.StudentGroups.Any()) 
                return Enumerable.Empty<StudentGroup>();

            return await dbContext.StudentGroups.ToListAsync();
        }

        public async Task<IEnumerable<StudentGroup>> GetStudentGroupByGroupId(int projectGroupId)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            return await dbContext.StudentGroups.Where(sg => sg.ProjectGroupId == projectGroupId).ToListAsync();
        }

        public async Task<IEnumerable<StudentGroup>> GetStudentGroupByStudentId(string studentId)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            return await dbContext.StudentGroups.Where(sg => sg.StudentId == studentId).ToListAsync();
        }


        public async Task<IEnumerable<StudentGroup>> GetStudentGroupsForSpecificSupervisorByJoinItWithProjectGrouppUsingId(string supervisorIdFromInterviewModel)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            return (from studentGroup in dbContext.StudentGroups
                                 join projectGroup in dbContext.ProjectGroups on studentGroup.ProjectGroupId equals projectGroup.Id
                                 where projectGroup.SupervisorId == supervisorIdFromInterviewModel
                                 select studentGroup).ToList();
        }


        public async Task AddStudentGroup(StudentGroup studentGroup)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            await dbContext.StudentGroups.AddAsync(studentGroup);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateStudentGroup(StudentGroup studentGroup)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            var studentGroupInDb = await dbContext.StudentGroups.FirstOrDefaultAsync(sg => sg.Id == studentGroup.Id);
            studentGroupInDb.StudentId = studentGroup.StudentId;
            studentGroupInDb.ProjectGroupId = studentGroup.ProjectGroupId;

            await dbContext.SaveChangesAsync();

        }

        public async Task RemoveStudentGroup(int studentGroupId)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            var studentGroupInDb = await dbContext.StudentGroups.FirstOrDefaultAsync(sg => sg.Id == studentGroupId);

            dbContext.StudentGroups.Remove(studentGroupInDb);
            await dbContext.SaveChangesAsync();

        }

        //....................................//End Of Students Groups............................................//


        //...................................//ProjectEvaluation..................................................//

        public async Task<IEnumerable<ProjectEvaluation>> GetProjectEvaluations()
        {
            //using var dbContext = _dbContextFactory.CreateDbContext();
            if (!dbContext.ProjectEvaluations.Any()) 
                return Enumerable.Empty<ProjectEvaluation>();

            return await dbContext.ProjectEvaluations.ToListAsync();
        }

        public async Task<ProjectEvaluation> GetSpecificProjectEvaluations(int projectGroupId)
        {
            return await dbContext.ProjectEvaluations.FirstOrDefaultAsync(pe => pe.ProjectGroupId == projectGroupId);
        }


        public async Task AddProjectEvaluation(ProjectEvaluation projectEvaluation)
        {
            var newEvaluation = new ProjectEvaluation()
            {
                Grade = projectEvaluation.Grade,
                GradeLevelName = projectEvaluation.GradeLevelName,
                ProjectGroupId = projectEvaluation.ProjectGroupId,
            };

            dbContext.ProjectEvaluations.Add(projectEvaluation);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateProjectEvaluation(ProjectEvaluation projectEvaluation)
        {
            var evaluationInDb = await dbContext.ProjectEvaluations.FirstOrDefaultAsync(pe => pe.Id == projectEvaluation.Id);
            evaluationInDb.Grade = projectEvaluation.Grade;
            evaluationInDb.GradeLevelName = projectEvaluation.GradeLevelName;
            evaluationInDb.ProjectGroupId = projectEvaluation.ProjectGroupId;

            await dbContext.SaveChangesAsync();
        }

        public async Task RemoveProjectEvaluation(int projectEvaluationId)
        {
            var evaluationInDb = await dbContext.ProjectEvaluations.FirstOrDefaultAsync(pe => pe.Id == projectEvaluationId);

            dbContext.ProjectEvaluations.Remove(evaluationInDb);
            await dbContext.SaveChangesAsync();
        }


        public string GetSpecificSupervisorForGroup(int projectEvaluationId)
        {
            return (from projectEvaluation in dbContext.ProjectEvaluations
                    join projectGroup in dbContext.ProjectGroups on projectEvaluation.ProjectGroupId equals projectGroup.Id
                    join user in dbContext.Users on projectGroup.SupervisorId equals user.Id
                    where projectEvaluation.Id == projectEvaluationId
                    select user.UserName).FirstOrDefault();

        }

        public string GetSpecificProjectForGroupByProjectGroupId(int projectGroupId)
        {
            return (from  projectGroup in dbContext.ProjectGroups
                    join project in dbContext.Projects on projectGroup.ProjectId equals project.Id
                    where projectGroup.Id == projectGroupId
                    select project.Title).FirstOrDefault();
        }

        public string GetUsernameForStudentGroupByGroupId(int projectGroupId, string studentId)
        {
            return (from  studentGroup in dbContext.StudentGroups
                    join user in dbContext.Users on studentGroup.StudentId equals user.Id
                    where studentGroup.ProjectGroupId == projectGroupId && studentGroup.StudentId == studentId
                    select user.UserName).SingleOrDefault();
        }

        public int GetProjectGroupIdByProjectEvaluationId(int projectEvaluationId)
        {
            return ( from projectgroup in dbContext.ProjectGroups 
                    join projectevaluation in dbContext.ProjectEvaluations on projectgroup.Id equals projectevaluation.ProjectGroupId
                    where projectevaluation.Id == projectEvaluationId
                     select projectgroup.Id).SingleOrDefault();
        }



        //.....................................Interview...................................................
        public async Task<IEnumerable<Interview>> GetInterviews()
        {
            if (!dbContext.Interviews.Any())
                return Enumerable.Empty<Interview>();

            return await dbContext.Interviews.ToListAsync();
        }

        public async Task<Interview> GetInterviewByProjectGroupId(int projectGroupId)
        {
            return await dbContext.Interviews.FirstOrDefaultAsync(i => i.StudentGroupId == projectGroupId);
        }

        public async Task<Interview> GetInterviewByItsId(int interviewId)
        {
            return await dbContext.Interviews.FirstOrDefaultAsync(i => i.Id == interviewId);
        }

        public async Task AddInterview(Interview interview)
        {
             await dbContext.Interviews.AddAsync(interview);

            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateInterview(Interview interview)
        {
            var interviewInDb = await dbContext.Interviews.FirstOrDefaultAsync(i => i.Id == interview.Id);

            interviewInDb.InterviewDate = interview.InterviewDate.Date;
            interviewInDb.InterviewTime = interview.InterviewTime;
            interviewInDb.StudentGroupId = interview.StudentGroupId;
            interview.SupervisorId = interview.SupervisorId;

            await dbContext.SaveChangesAsync();
        }

        public async Task RemoveInterview(int interviewId)
        {
                var interviewInDb = await dbContext.Interviews.FirstOrDefaultAsync(i => i.Id == interviewId);

                dbContext.Interviews.Remove(interviewInDb);

                await dbContext.SaveChangesAsync();
        }



        //...................................//End Of ProjectEvaluation..................................................//

    }
}

