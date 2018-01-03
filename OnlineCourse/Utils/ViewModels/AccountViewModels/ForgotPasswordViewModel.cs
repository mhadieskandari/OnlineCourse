using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Panel.Utils.ViewModels.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [DataType(DataType.PhoneNumber,ErrorMessage = "PublicNotValid")]
        [Required(ErrorMessage = "PublicRequireValidation")]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }
    }
}
