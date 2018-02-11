using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineCourse.Entity.Models
{
    public class ClassRoom
    {
        [Key]
        public int Id { get; set; }
        public int PresentId { get; set; }
        public TimeSpan StartedTime { get; set; }
        public TimeSpan EndedTime { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public ClassStatus Status { get; set; }
        public string Source { get; set; }
        public byte? ChangeTimePermit { get; set; }  //ValidationState

        [ForeignKey("PresentId")]
        public Present Present { get; set; }
    }
}
