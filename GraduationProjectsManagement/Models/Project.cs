using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProjectsManagement.Models
{ 
    public class Project
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "الرجاء ملء الحقل")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "الرجاء ملء الحقل")]
        public string Description { get; set; } = string.Empty;
        public DateTime? AddedDate { get; set; }

        public string Image { get; set; } = string.Empty;

        public byte[]? ImageFile { get; set; }

        public bool IsSelected { get; set; }
        public ProjectCategory ProjectCategory { get; } = new();

        public ProjectType ProjectType { get; } = new();

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; } = new();

        [Required(ErrorMessage = "الرجاء ملء الحقل")]
        public int ProjectTypeId { get; set; }

        [Required(ErrorMessage = "الرجاء ملء الحقل")]
        public int ProjectCategoryId { get; set; }
        public string UserId { get; set; }

    }
}
