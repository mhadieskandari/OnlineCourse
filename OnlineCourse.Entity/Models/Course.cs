using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineCourse.Entity.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "نام درس")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Name { get; set; }

        [Display(Name = "مقطع/پایه تحصیلی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public EducationLevel Level { get; set; }

        public ICollection<Section> Sections { set; get; }

        [Display(Name = "نام درس")]
        public string CourseName => Name + " " + EnumExtentionEntity.GetDescription(Level);
    }
}
