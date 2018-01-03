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
        [Display(Name = "Email")]
        public string Email { set; get; }
        public string Ip { set; get; }
        public string ReturnUrl { get; set; }

        [Display(Name= "VerificationCode")]
        public string Code { set; get; }
    }
}
