using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Panel.Utils.ViewModels.AccountViewModels
{
    public class SendCodeViewModel
    {
        //public string SelectedProvider { get; set; }

        //public ICollection<SelectListItem> Providers { get; set; }

        //public string ReturnUrl { get; set; }

        //public bool RememberMe { get; set; }
        [EmailAddress]
        [Display(Name = "ایمیل/نام کاربری")]
        public string Email { set; get; }
        public string Ip { set; get; }
        public string ReturnUrl { get; set; }

        [Display(Name= "کد فعال سازی")]
        public string Code { set; get; }
    }
}
