using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourse.Entity.Models
{
    public class Term
    {
        public int Id { get; set; }
        public short Year { get; set; }
        public short YearTerm { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public TermType Type { get; set; }
    }
}
