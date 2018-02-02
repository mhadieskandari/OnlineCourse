using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineCourse.Entity.Models
{
    public class Gallery
    {
        public int Id { set; get; }

        [Display(Name = "Title")]
        [MaxLength(100)]
        public string Title { set; get; }

        [Display(Name = "Description")]
        [MaxLength(400)]
        public string Des { set; get; }

        [Display(Name = "Extention")]
        [MaxLength(4)]
        public string Ext { set; get; }

        [Display(Name = "Priority")]
        public byte? POrder { set; get; }

        [Display(Name = "PublicId")]
        public int PublicId { set; get; }

        [Display(Name = "Kind")]
        public byte? Kind { set; get; }

        [Display(Name = "State")]
        public byte? State { set; get; }

    }
}
