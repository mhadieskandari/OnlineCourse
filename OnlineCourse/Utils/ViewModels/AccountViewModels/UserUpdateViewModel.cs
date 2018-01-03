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

        [Required(ErrorMessage = "PublicRequireValidation")]
        [Display(Name = "UserName")]
        [MaxLength(100)]
        public string UserName { set; get; }

        [Required(ErrorMessage = "PublicRequireValidation")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "PublicNotValid")]
        [MaxLength(100)]
        public string Email { set; get; }

        [Required(ErrorMessage = "PublicRequireValidation")]
        [MaxLength(20)]
        [Display(Name = "FullName")]
        public string FullName { set; get; }

        //[Required(ErrorMessage = "لطفا رمز عبور را وارد نمائید.")]
        //[Display(Name = "رمز عبور")]
        //[StringLength(100, ErrorMessage = "{0} حداقل  {2} کاراکتر و حداکثر {1} کاراکتر باید باشد.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //public string Pwd { get; set; }

        //[Display(Name = "تلفن همراه")]
        //[MaxLength(50)]
        //public string Mobile { get; set; }

        [Display(Name = "Phone")]
        [MaxLength(50)]
        public string Phone { get; set; }

        [Display(Name = "Addrress")]
        [MaxLength(200)]
        public string Addrress { set; get; }

        [Display(Name = "Position")]
        [MaxLength(30)]
        public string Position { set; get; }

        [Display(Name = "City")]
        [MaxLength(50)]
        public string City { set; get; } = "Sydney";

        [MaxLength(50)]
        [Display(Name = "Country")]
        public string Country { set; get; } = "Australia";



    }
}
