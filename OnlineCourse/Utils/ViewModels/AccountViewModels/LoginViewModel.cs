

using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Panel.Utils.ViewModels.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "{0} را وارد کنید.")]
        [EmailAddress(ErrorMessage = "{0} را به طور صحیح وارد کنید")]
        [Display(Name = "نام کاربری/ایمیل ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} را وارد کنید.")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [Display(Name = "مرا بخاطر بسپار؟")]
        public bool RememberMe { get; set; }
    }
}
