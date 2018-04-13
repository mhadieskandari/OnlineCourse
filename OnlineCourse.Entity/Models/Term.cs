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
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Title { get; set; }
        [Display(Name = "سال")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public short Year { get; set; }
        [Display(Name = "نیمه")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public short YearTerm { get; set; }
        [Display(Name = "توضیحات")]
        public string Description { get; set; }
        [Display(Name = "تاریخ شروع")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string StartDate { get; set; }
        [Display(Name = "تاریخ پایان")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string EndDate { get; set; }
        [Display(Name = "نوع ترم")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public TermType Type { get; set; }
        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public TermState State { set; get; }

        public ICollection<Section> Sections { set; get; }
    }
}
