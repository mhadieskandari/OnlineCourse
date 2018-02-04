using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourse.Entity.Models
{
    public class Section
    {
        public int Id { get; set; }
        public decimal TotalTime { get; set; }
        public decimal HourlyPrice { get; set; }
        public Course Course { get; set; }
        public int CourseId { get; set; }
        public Term Term { get; set; }
        public int TermId { get; set; }
        public User Teacher { get; set; }
        public int TeacherId { get; set; }
    }
}
