using Microsoft.AspNetCore.Identity;

namespace GraduationProjectsManagement.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public byte[]? ImageFile { get; set; }
    }
}
