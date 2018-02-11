﻿using System;
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
        public int StudentId { get; set; }
        public int TermId { get; set; }

        [ForeignKey("StudentId")]
        public User Student { get; set; }

        [ForeignKey("TermId")]
        public Term Term { get; set; }
    }
}
