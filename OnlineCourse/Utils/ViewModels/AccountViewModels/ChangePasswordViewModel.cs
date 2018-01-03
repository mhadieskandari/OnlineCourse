using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourse.Panel.Utils.ViewModels.AccountViewModels
{
    public class ChangePasswordViewModel
    {
        public int Id { set; get; }

        [Required(ErrorMessage = "PublicRequireValidation")]
        [Display(Name = "OldPass")]
        [StringLength(100, ErrorMessage = "PublicStringLengthValidation", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string OldPass { get; set; }

        [Required(ErrorMessage = "PublicRequireValidation")]
        [Display(Name = "NewPass")]
        [StringLength(100, ErrorMessage = "PublicStringLengthValidation", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPass { get; set; }

        [Required(ErrorMessage = "PublicRequireValidation")]
        [Display(Name = "ConfirmNewPass")]
        [StringLength(100, ErrorMessage = "PublicStringLengthValidation", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare("NewPass",ErrorMessage = "ConfirmPasswordValidation")]
        public string ConfirmNewPass { get; set; }


    }
}
