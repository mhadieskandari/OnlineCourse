using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Panel.Utils.ViewModels.Areas.Admin
{
    public class AddPresentScedulViewModel
    {
        [Key]
        public int SectionId { get; set; }
        [Required(ErrorMessage = "{0} اجباریست", AllowEmptyStrings = false)]
        [Display(Name = "روز هفته")]
        public string WorkDays { get; set; }
        [Required(ErrorMessage = "{0} اجباریست")]
        [Display(Name = "ساعت شروع")]
        public string StartTime { get; set; }
        [Display(Name = "ساعت پایان")]
        [Required(ErrorMessage = "{0} اجباریست")]
        public string EndTime { get; set; }
    }
}
