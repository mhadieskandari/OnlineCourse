using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineCourse.Entity.Models
{
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "تخفیف")]
        [DisplayFormat(DataFormatString = "{0:N0}",ApplyFormatInEditMode = true,ConvertEmptyStringToNull =true)]
        public decimal Markdown { get; set; }
        public int PresentId { get; set; }
        [Display(Name = "وضعیت")]
        public EnrollmentState State { get; set; }        
        [ForeignKey("PresentId")]
        public Present Present { get; set; }
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public User Student { get; set; }
        public int InvoiceId { set; get; }
        [ForeignKey("InvoiceId")]
        public Invoice Invoice { set; get; }
        public ICollection<Payment> Payments { set; get; }

    }
}
