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
        public decimal TotalTime { get; set; }
        public decimal HourlyPrice { get; set; }
        public int CourseId { get; set; }
        public int TermId { get; set; }
        public int TeacherId { get; set; }
        public ActiveState? Activity { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        [ForeignKey("TeacherId")]
        public User Teacher { get; set; }

        [ForeignKey("TermId")]
        public Term Term { get; set; }
    }
}
