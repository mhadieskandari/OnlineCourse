using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineCourse.Entity.Models
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        public WeekDays DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int PresentId { get; set; }

        [ForeignKey("PresentId")]
        public Present Present { get; set; }
    }
}
