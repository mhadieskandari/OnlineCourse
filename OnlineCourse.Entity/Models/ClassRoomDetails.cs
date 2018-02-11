using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineCourse.Entity.Models
{
    public class ClassRoomDetails
    {
        [Key]
        public int Id { get; set; }
        public int ClassRoomId { get; set; }
        public int StudentId { get; set; }
        public string Source { get; set; }
        public ClassDocKind Kind { get; set; }

        [ForeignKey("StudentId")]
        public User Student { get; set; }

        [ForeignKey("ClassRoomId")]
        public ClassRoom ClassRoom { get; set; }
    }
}
