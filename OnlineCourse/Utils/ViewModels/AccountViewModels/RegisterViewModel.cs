using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Panel.Utils.ViewModels.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "{0} را به طور صحیح وارد کنید")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} را وارد کنید")]
        [Display(Name = "نام و نام خانوادگی")]
        public string FullName { set; get; }


        [Required(ErrorMessage = "{0} را وارد کنید")]
        [Display(Name = "شماره همراه")]
        public string Mobile { set; get; }

        [Required(ErrorMessage = "{0} را وارد کنید")]
        [StringLength(100, ErrorMessage = "{0} باید بین {1} تا {2} کاراکتر باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        //[Required(ErrorMessage = "PublicRequireValidation")]
        //[DataType(DataType.Password)]
        //[Display(Name = "ConfirmPassword")]
        //[Compare("Password", ErrorMessage = "ConfirmPasswordValidation")]
        //public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "{0} را وارد کنید")]
        [Display(Name = "شهر")]
        //[Required(ErrorMessage = "لطفا شهر را وارد کنید.")]
        public string City { get; set; }

        [Required(ErrorMessage = "{0} را وارد کنید")]
        [Display(Name = "نوع کابر")]
        [Range(0,1,ErrorMessage = "{0} اشتباه است")]
        public byte? IsTeacher { set; get; }
    }
}
