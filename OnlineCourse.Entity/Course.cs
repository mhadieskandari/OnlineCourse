using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourse.Entity
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EducationLevel Level { get; set; }
    }
}
