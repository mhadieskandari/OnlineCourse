using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineCourse.Entity.Models
{
    public class Term
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="عنوان")]
        public string Title { get; set; }
        [Display(Name = "سال")]
        public short Year { get; set; }
        [Display(Name = "نیمه")]
        public short YearTerm { get; set; }
        [Display(Name = "توضیحات")]
        public string Description { get; set; }
        [Display(Name = "تاریخ شروع")]
        public string StartDate { get; set; }
        [Display(Name = "تاریخ پایان")]
        public string EndDate { get; set; }
        [Display(Name = "نوع ترم")]
        public TermType Type { get; set; }
        [Display(Name = "وضعیت")]
        public TermState State { set; get; }

        public ICollection<Section> Sections { set; get; }
    }
}
