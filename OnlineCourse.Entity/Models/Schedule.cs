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
        [Required(ErrorMessage = "{0} اجباریست",AllowEmptyStrings = false)]
        [Display(Name = "روز هفته")]
        public WeekDays DayOfWeek { get; set; }
        [Required(ErrorMessage = "{0} اجباریست")]
        [Display(Name = "ساعت شروع")]
        public TimeSpan StartTime { get; set; }
        [Display(Name = "ساعت پایان")]
        [Required(ErrorMessage = "{0} اجباریست")]
        public TimeSpan EndTime { get; set; }
        public int PresentId { get; set; }

        [ForeignKey("PresentId")]
        public Present Present { get; set; }
    }
}
