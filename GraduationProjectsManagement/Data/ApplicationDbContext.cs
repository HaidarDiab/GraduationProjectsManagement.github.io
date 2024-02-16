using GraduationProjectsManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace GraduationProjectsManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext <IdentityUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> AspNetUsers { get; set; }

        public DbSet<Project> Projects => Set<Project>();

        public DbSet<ProjectCategory> ProjectCategories => Set<ProjectCategory>();
        public DbSet<ProjectType> ProjectTypes => Set<ProjectType>();
        public DbSet<ProjectGroup> ProjectGroups => Set<ProjectGroup>();
        public DbSet<StudentGroup> StudentGroups => Set<StudentGroup>();
        public DbSet<ProjectEvaluation> ProjectEvaluations => Set<ProjectEvaluation>();
        public DbSet<Blog> Blogs => Set<Blog>();
        public DbSet<DiscussionBlog> DiscussionBlogs => Set<DiscussionBlog>();
        public DbSet<MailStatus> MailStatuses => Set<MailStatus>();
        public DbSet<ReceivedMail> ReceivedMails => Set<ReceivedMail>();
        public DbSet<SentMail> SentMails => Set<SentMail>();
        public DbSet<Interview> Interviews => Set<Interview>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(t => t.UserId);
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(x => x.UserId);
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(x => new { x.UserId, x.RoleId });
            //modelBuilder.Entity<IdentityRoleClaim<string>>().HasKey(x => x.Id);

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole()
            {
                Id = "Admin12345",
                Name = "Admin"
            });

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole()
            {
                Id = "Supervisor12345",
                Name = "Supervisor"
            });

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole()
            {
                Id = "Student12345",
                Name = "Student"
            });

            var hasher = new PasswordHasher<ApplicationUser>();

            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser()
            {
                Id = "rahafabdo",
                FirstName = "Admin",
                MiddleName = "Admin",
                LastName = "Admin",
                UserName = "admin@svuonline.org",
                Email = "admin@svuonline.org",
                PasswordHash = hasher.HashPassword(null, "Admin12345@")
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new[]
            {
                new IdentityUserRole<string>() { UserId = "rahafabdo", RoleId = "1" },
            });




            modelBuilder.Entity<ProjectType>().HasData(new ProjectType()
            {
                Id = 1,
                Type = "مقترح مشروع"
            });

            modelBuilder.Entity<ProjectType>().HasData(new ProjectType()
            {
                Id = 2,
                Type = "مشروع فصلي"
            });

            modelBuilder.Entity<MailStatus>().HasData(new MailStatus()
            {
                Id = 1,
                Name = "صادر"
            });

            modelBuilder.Entity<MailStatus>().HasData(new MailStatus()
            {
                Id = 2,
                Name = "وارد"
            });
        }

    }
}