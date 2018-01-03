

using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Panel.Utils.ViewModels.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "RequireEmail")]
        [EmailAddress(ErrorMessage = "EmailAddressNotValidFormat")]
        [Display(Name = "EmailUserName")]
        public string Email { get; set; }

        [Required(ErrorMessage = "RequirePassword")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "RememberMe")]
        public bool RememberMe { get; set; }
    }
}
