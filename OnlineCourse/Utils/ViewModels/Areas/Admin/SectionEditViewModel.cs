﻿using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineCourse.Entity;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OnlineCourse.Panel.Utils.ViewModels.Areas.Admin
{
    public class SectionEditViewModel
    {
        public SectionEditViewModel(ApplicationDbContext context)
        {
            Teachers =new SelectList( context.Users.Where(u=>u.AccessLevel==UserAccessLevel.Teacher).ToList(),"Id","FullName");
            Terms =new SelectList( context.Terms.ToList(),"Id" , "Title");
            Courses =new SelectList( context.Courses.ToList(),"Id", "CourseName");
        }
        public SectionEditViewModel()
        {
        }

        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:0,0}", ApplyFormatInEditMode = true)]
        [Display(Name = "زمان کل(ساعت)")]
        public decimal TotalTime { get; set; }
        [DisplayFormat(DataFormatString = "{0:0,0}", ApplyFormatInEditMode = true)]
        [Display(Name = "هزینه ساعتی(تومان)")]
        public decimal HourlyPrice { get; set; }
        [Display(Name = "نام درس")]
        [Required(ErrorMessage = "{0} اجباریست")]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "{0} اجباریست")]
        [Display(Name = "نام ترم")]
        public int TermId { get; set; }
        [Required(ErrorMessage = "{0} اجباریست")]
        [Display(Name = "نام استاد")]
        public int TeacherId { get; set; }
        [Required(ErrorMessage = "{0} اجباریست")]
        [Display(Name = "وضعیت")]
        public ActiveState? Activity { get; set; }
        
        public SelectList Courses { set; get; }
        public SelectList Terms { set; get; }
        public SelectList Teachers { set; get; }

        public void IsEdit(ApplicationDbContext context)
        {
            Teachers = new SelectList(context.Users.Where(u => u.AccessLevel == UserAccessLevel.Teacher).ToList(), "Id", "FullName",TeacherId);
            Terms = new SelectList(context.Terms.ToList(), "Id", "Title",TermId);
            Courses = new SelectList(context.Courses.ToList(), "Id", "CourseName", CourseId);
        }

    }
}
