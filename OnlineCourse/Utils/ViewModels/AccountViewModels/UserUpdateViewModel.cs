using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourse.Panel.Utils.ViewModels.AccountViewModels
{
    public class UserUpdateViewModel
    {
        public int Id { set; get; }

        [Required(ErrorMessage = "{0} را وارد کنید.")]
        [Display(Name = "نام کاربری")]
        [MaxLength(100)]
        public string UserName { set; get; }

        [Required(ErrorMessage = "{0} را وارد کنید.")]
        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "{0} نامعتبر است.")]
        [MaxLength(100)]
        public string Email { set; get; }

        [Required(ErrorMessage = "{0} را وارد کنید.")]
        [MaxLength(20)]
        [Display(Name = "نام و نام خانوادگی")]
        public string FullName { set; get; }

        [Display(Name = "تلفن ثابت")]
        [MaxLength(50)]
        public string Phone { get; set; }

        [Display(Name = "آدرس")]
        [MaxLength(200)]
        public string Addrress { set; get; }

        [Display(Name = "شهر")]
        [MaxLength(50)]
        [Required(ErrorMessage = "{0} را وارد کنید.")]
        public string City { set; get; } 




    }
}
