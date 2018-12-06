using Microsoft.AspNetCore.Http;
using OnlineCourse.Panel.Utils.HtmlHelper;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineCourse.Entity;

namespace OnlineCourse.Panel.Utils.ViewModels.Areas.Admin
{

    public class CreateUserViewModel
    {

        public int Id { set; get; }

        [MaxLength(100)]
        [Display(Name = "نام کاربری")]
        public string UserName { set; get; }

        [MaxLength(100)]
        [Display(Name = "ایمیل")]
        public string Email { set; get; }

        [MaxLength(20)]
        [Display(Name = "نام و نام خانوادگی")]
        public string FullName { set; get; }

        [MaxLength(200)]
        [Display(Name = "رمز عبور")]
        public string Pwd { get; set; }

        [MaxLength(50)]
        [Display(Name = "تلفن همراه")]
        public string Mobile { get; set; }

        [MaxLength(400)]
        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "سطح دسترسی")]
        public UserAccessLevel AccessLevel { get; set; }

        [Display(Name = "وضعیت")]
        public UserState State { get; set; }

        [Column(TypeName = "datetime")]
        [Display(Name = "تاریخ انقضا")]
        [DataType(DataType.Text)]
        public DateTime? ExpireDate { get; set; }

        [MaxLength(200)]
        [Display(Name = "آدرس")]
        public string Addrress { set; get; }

        [MaxLength(50)]
        [Display(Name = "شهر")]
        public string City { set; get; }

        [Display(Name = "تصویر پروفایل")]
        [UploadFileExtensions(".png,.jpg,.jpeg,.gif", ErrorMessage = " نوع {0} باید یکی از .png,.jpg,.jpeg,.gif این فرمتها باشد.")]
        [DataType(DataType.Upload)]
        public IFormFile Image { set; get; }



    }
}
