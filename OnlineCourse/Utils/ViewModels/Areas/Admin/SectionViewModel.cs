using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineCourse.Entity;
using OnlineCourse.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourse.Panel.Utils.ViewModels.Areas.Admin
{
    public class SectionViewModel
    {
        public SectionViewModel(ApplicationDbContext context)
        {
            Teachers =new SelectList( context.Users,"Id","FullName");
            Terms =new SelectList( context.Terms.ToList(),"Id" , "Description");
            Courses =new SelectList( context.Courses.ToList(),"Id","Name");
        }
        public SectionViewModel()
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
        public int CourseId { get; set; }
        [Display(Name = "نام ترم")]
        public int TermId { get; set; }
        [Display(Name = "نام استاد")]
        public int TeacherId { get; set; }
        [Display(Name = "وضعیت")]
        public ActiveState? Activity { get; set; }

        public SelectList Courses { set; get; }
        public SelectList Terms { set; get; }
        public SelectList Teachers { set; get; }

        public void IsEdit(ApplicationDbContext context)
        {
            Teachers = new SelectList(context.Users, "Id", "FullName",TeacherId);
            Terms = new SelectList(context.Terms.ToList(), "Id", "Description",TermId);
            Courses = new SelectList(context.Courses.ToList(), "Id", "Name",CourseId);
        }
    }
}
