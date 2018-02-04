using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourse.Entity.Models
{
    public class EnrollmentDetails
    {
        public int Id { get; set; }
        public decimal Markdown { get; set; }
        public Enrollment Enrollment { get; set; }
        public int EnrollmentId { get; set; }
        public Section Section { get; set; }
        public int SectionId { get; set; }

    }
}
