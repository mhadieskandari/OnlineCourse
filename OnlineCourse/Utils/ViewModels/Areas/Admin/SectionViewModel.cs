using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineCourse.Entity;
using OnlineCourse.Entity.Models;
using System;
using System.Collections.Generic;
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
        public int Id { get; set; }
        public decimal TotalTime { get; set; }
        public decimal HourlyPrice { get; set; }
        public int CourseId { get; set; }
        public int TermId { get; set; }
        public int TeacherId { get; set; }
        public ActiveState? Activity { get; set; }


        public SelectList Courses { set; get; }
        public SelectList Terms { set; get; }
        public SelectList Teachers { set; get; }
        //public List<Course> Courses { set; get; }
        //public List<Term> Terms { set; get; }
        //public List<User> Teachers { set; get; }
    }
}
