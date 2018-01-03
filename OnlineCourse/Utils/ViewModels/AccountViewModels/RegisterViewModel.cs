using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Panel.Utils.ViewModels.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "PublicRequireValidation")]
        [EmailAddress(ErrorMessage = "EmailAddressNotValidFormat")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PublicRequireValidation")]
        [Display(Name = "FullName")]
        public string FullName { set; get; }


        [Required(ErrorMessage = "PublicRequireValidation")]
        [Display(Name = "Mobile")]
        public string Mobile { set; get; }

        [Required(ErrorMessage = "PublicRequireValidation")]
        [StringLength(100, ErrorMessage = "PublicStringLengthValidation", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        //[Required(ErrorMessage = "PublicRequireValidation")]
        //[DataType(DataType.Password)]
        //[Display(Name = "ConfirmPassword")]
        //[Compare("Password", ErrorMessage = "ConfirmPasswordValidation")]
        //public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "PublicRequireValidation")]
        [Display(Name = "City")]
        //[Required(ErrorMessage = "لطفا شهر را وارد کنید.")]
        public string City { get; set; } = "Sydney";

        [Required(ErrorMessage = "PublicRequireValidation")]
        [Display(Name = "AccountType")]
        [Range(0,1,ErrorMessage = "WrongUserKindValidation")]
        public byte? IsTeacher { set; get; }
    }
}
