using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineCourse.Entity.Models
{
    public class User
    {

        [Key]
        public int Id { set; get; }

        [Display(Name = "نام کاربری")]
        [MaxLength(100)]
        public string UserName { set; get; }

        [Display(Name = "ایمیل")]
        [MaxLength(100)]
        public string Email { set; get; }

        [Display(Name = "نام و نام خانوادگی")]
        [MaxLength(20)]
        public string FullName { set; get; }

        [Display(Name = "رمز عبور")]
        [MaxLength(200)]
        public string Password { get; set; }

        [Display(Name = "تلفن همراه")]
        [MaxLength(50)]
        public string Mobile { get; set; }

        [Display(Name = "توضیحات")]
        [MaxLength(400)]
        public string Description { get; set; }

        [Display(Name = "سطح دسترسی")]
        public UserAccessLevel AccessLevel { get; set; }

        [Display(Name = "تلفن ثابت")]
        [MaxLength(50)]
        public string Phone { get; set; }

        [Display(Name = "وضعیت")]
        public UserState State { get; set; }

        [Display(Name = "کد فعال سازی")]
        [MaxLength(20)]
        public string ActivationCode { get; set; }

        [Display(Name = "آخرین درخواست کد فعال سازی")]
        [Column(TypeName = "datetime")]
        [DataType(DataType.Date)]
        public DateTime? LastRequestActivationCode { get; set; }

        public byte? ValidMobile { get; set; }

        public byte? ValidEmail { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        [Column(TypeName = "datetime")]
        [DataType(DataType.Date)]
        public DateTime? RegisterDate { get; set; }

        [Display(Name = "آخرین ip ورود")]
        [MaxLength(20)]
        public string LastLoginIp { get; set; }

        [Display(Name = "LastLoginDate")]
        [Column(TypeName = "datetime")]
        [DataType(DataType.Date)]
        public DateTime? LastLoginDate { get; set; }

        [Display(Name = "تاریخ انقضا")]
        [Column(TypeName = "datetime")]
        [DataType(DataType.Text)]
        public DateTime? ExpireDate { get; set; }

        [MaxLength(100)]
        public string MobileCreditAlarm { get; set; }

        public int? LastVisitedNewsId { get; set; }

        public int? RegisterAttemptFailure { get; set; }

        public int? LoginAttemptFailure { get; set; }

        [Column(TypeName = "datetime")]
        [DataType(DataType.Date)]
        public DateTime? LastResetPasswordDate { get; set; }

        [Display(Name = "آدرس")]
        [MaxLength(200)]
        public string Addrress { set; get; }
        
        [Display(Name = "شهر")]
        [MaxLength(50)]
        public string City { set; get; }

        public string SecuritySpan { set; get; }

        [Display(Name = "تاریخ تولد")]
        [Column(TypeName = "datetime")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "مدرک تحصیلی")]
        public EducationLevel? Degree { get; set; }

        public ICollection<ClassRoomDetails> ClassRoomDetails { set; get; }

        public ICollection<Section> Sections { set; get; }
    }


}
