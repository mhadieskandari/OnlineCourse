using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourse.Entity.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public Enrollment Enrollment { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public PayType Type { get; set; }
    }
}
