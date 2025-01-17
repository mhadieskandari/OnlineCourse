﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineCourse.Entity.Models
{
    public class Present
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int SectionId { get; set; }

        [ForeignKey("SectionId")]
        public Section Section { get; set; }

        public ICollection<Enrollment> Enrollments { set; get; }

        public ICollection<Schedule> Schedules { set; get; }

        public ICollection<ClassRoom> ClassRooms { set; get; }

    }
}
