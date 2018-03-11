using OnlineCourse.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourse.Panel.Utils.ViewModels.Areas.Admin
{
    public class TermSearchViewModel
    {
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        [Display(Name = "سال")]
        public short? Year { get; set; }
        [Display(Name = "نیمه")]
        public short? YearTerm { get; set; }
        [Display(Name = "توضیحات")]
        public string Description { get; set; }
        [Display(Name = "تاریخ شروع")]
        public string StartDate { get; set; }
        [Display(Name = "تاریخ پایان")]
        public string EndDate { get; set; }
        [Display(Name = "نوع ترم")]
        public TermType? Type { get; set; }
        [Display(Name = "وضعیت")]
        public TermState? State { set; get; }
    }
}
