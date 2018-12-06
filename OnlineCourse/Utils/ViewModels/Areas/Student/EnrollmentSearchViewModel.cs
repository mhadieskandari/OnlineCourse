using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineCourse.Core.Services;
using OnlineCourse.Entity;

namespace OnlineCourse.Panel.Utils.ViewModels.Areas.Student
{
    public class EnrollmentSearchViewModel
    {
        public EnrollmentSearchViewModel(ApplicationDbContext context,CurrentUser user)
        {
            Courses = new SelectList(context.Courses.ToList(), "Id", "CourseName");
            Teachers = new SelectList(context.Users.Where(u => u.AccessLevel == UserAccessLevel.Teacher).ToList(), "Id", "FullName");
            Terms = new SelectList(context.Terms.ToList(), "Id", "Title");
        }

        [Display(Name = "نام درس")]
        public int CourseId { get; set; }
        [Display(Name = "نام ترم")]
        public int TermId { get; set; }
        [Display(Name = "نام استاد")]
        public int TeacherId { get; set; }
        [Display(Name = "وضعیت")]
        public EnrollmentState? State { get; set; }

        public SelectList Courses { get; set; }
        public SelectList Terms { get; set; }
        public SelectList Teachers { get; set; }
    }
}
