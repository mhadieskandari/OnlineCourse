using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Panel.Utils.ViewModels.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "RequirePassword")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "RequirePassword")]
        [StringLength(100, ErrorMessage = "PublicStringLengthValidation", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessage = "ConfirmPasswordValidation")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "VerificationCode")]
        public string Code { get; set; }
    }
}
