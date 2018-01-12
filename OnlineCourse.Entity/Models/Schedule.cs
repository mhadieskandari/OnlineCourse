using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourse.Entity.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public WeekDays DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public Section Section { get; set; }
    }
}
