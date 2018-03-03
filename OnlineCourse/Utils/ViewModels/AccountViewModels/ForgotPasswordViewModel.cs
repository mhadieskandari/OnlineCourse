using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Panel.Utils.ViewModels.AccountViewModels
{
    public class ForgotPasswordViewModel
    {

        [Required(ErrorMessage = "{0} را وارد کنید")]
        [Display(Name = "شماره همراه")]
        public string Mobile { set; get; }
    }
}
