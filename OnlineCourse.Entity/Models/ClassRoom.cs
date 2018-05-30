using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineCourse.Entity.Models
{
    public class ClassRoom
    {
        [Key]
        public int Id { get; set; }
        public int PresentId { get; set; }
        [Display(Name = "زمان شروع")]
        public TimeSpan StartedTime { get; set; }
        [Display(Name = "زمان پایان")]
        public TimeSpan EndedTime { get; set; }
        [Display(Name = "تاریخ")]
        public DateTime Date { get; set; }
        [Display(Name = "توضیحات")]
        public string Description { get; set; }
        [Display(Name = "وضعیت کلاس")]
        public ClassStatus Status { get; set; }
        [Display(Name = "فایل")]
        public string Source { get; set; }
        public byte? ChangeTimePermit { get; set; }  //ValidationState

        [ForeignKey("PresentId")]
        public Present Present { get; set; }
    }
}
