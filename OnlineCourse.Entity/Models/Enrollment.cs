﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourse.Entity.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public User Student { get; set; }
        public int StudentId { get; set; }
        public Term Term { get; set; }
        public int TermId { get; set; }
    }
}
