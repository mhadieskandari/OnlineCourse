using OnlineCourse.Core.Extentions;
using OnlineCourse.Entity;
using OnlineCourse.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourse.Panel.Utils.ViewModels.Areas.Admin
{
    public class SectionSearchViewModel
    {
        public SectionSearchViewModel(ApplicationDbContext context)
        {
            Courses = context.Courses.ToList();
            Teachers = context.Users.Where(u => u.AccessLevel == UserAccessLevel.Teacher).ToList();
            Terms = context.Terms.ToList();
        }

        [Display(Name = "نام درس")]
        public int CourseId { get; set; }
        [Display(Name = "نام ترم")]
        public int TermId { get; set; }
        [Display(Name = "نام استاد")]
        public int TeacherId { get; set; }
        [Display(Name = "وضعیت")]
        public ActiveState? Activity { get; set; }

        public List<Course> Courses { get; set; }
        public List<Term> Terms { get; set; }
        public List<User> Teachers { get; set; }
        public List<ActiveState>  Activities { get; set; }
    }
}
