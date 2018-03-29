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

        [Required(ErrorMessage = "{0} اجباریست.")]
        [Display(Name = "رمز عبور قبلی")]
        [StringLength(100, ErrorMessage = "حداقل طول {0} باید {1} کاراکتر باید باشد.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string OldPass { get; set; }

        [Required(ErrorMessage = "{0} اجباریست.")]
        [Display(Name = "رمز عبور جدید")]
        [StringLength(100, ErrorMessage = "حداقل طول {0} باید {1} کاراکتر باید باشد.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPass { get; set; }

        [Required(ErrorMessage = "{0} اجباریست.")]
        [Display(Name = "تکرار رمز عبور جدید")]
        [StringLength(100, ErrorMessage = "حداقل طول {0} باید {1} کاراکتر باید باشد.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare("NewPass",ErrorMessage = "{0} با {1}همخوانی ندارد.")]
        public string ConfirmNewPass { get; set; }


    }
}
