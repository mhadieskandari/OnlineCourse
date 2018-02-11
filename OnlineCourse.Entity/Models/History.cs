using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineCourse.Entity.Models
{

    public class History
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
        public int? UserId { get; set; }
        public string Os { get; set; }
        public string Browser { get; set; }
        public string Ip { get; set; }
        public DateTime? Date { get; set; }
        public string Message { get; set; }
        public byte? Action { get; set; }
        public byte? State { get; set; }
    }
}
