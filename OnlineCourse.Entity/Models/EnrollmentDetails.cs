﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineCourse.Entity.Models
{
    public class EnrollmentDetails
    {
        [Key]
        public int Id { get; set; }
        public decimal Markdown { get; set; }
        public int EnrollmentId { get; set; }
        public int PresentId { get; set; }
        public ActiveState Activity { get; set; }

        [ForeignKey("EnrollmentId")]
        public Enrollment Enrollment { get; set; }

        [ForeignKey("PresentId")]
        public Present Present { get; set; }
    }
}
