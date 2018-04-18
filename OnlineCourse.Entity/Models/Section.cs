using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineCourse.Entity.Models
{
    public class Section
    {
        [Key]
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:0,0}", ApplyFormatInEditMode = true)]
        [Display(Name ="زمان کل")]
        public decimal TotalTime { get; set; }
        [DisplayFormat(DataFormatString = "{0:0,0}", ApplyFormatInEditMode = true)]
        [Display(Name = "هزینه ساعتی")]
        public decimal HourlyPrice { get; set; }
        [Display(Name = "نام درس")]
        public int CourseId { get; set; }
        [Display(Name = "نام ترم")]
        public int TermId { get; set; }
        [Display(Name = "نام استاد")]
        public int TeacherId { get; set; }
        [Display(Name = "وضعیت")]
        public ActiveState? Activity { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        [ForeignKey("TeacherId")]
        public User Teacher { get; set; }

        [ForeignKey("TermId")]
        public Term Term { get; set; }

        [Display(Name = "هزینه کلاس")]
        public decimal TotalCost => TotalTime * HourlyPrice;

        public ICollection<Present> Presents { set; get; }
    }
}
