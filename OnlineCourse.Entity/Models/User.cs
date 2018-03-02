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

        [Display(Name = "UserName")]
        [MaxLength(100)]
        public string UserName { set; get; }

        [Display(Name = "Email")]
        [MaxLength(100)]
        public string Email { set; get; }

        [Display(Name = "FullName")]
        [MaxLength(20)]
        public string FullName { set; get; }

        [Display(Name = "Password")]
        [MaxLength(200)]
        public string Password { get; set; }

        [Display(Name = "Mobile")]
        [MaxLength(50)]
        public string Mobile { get; set; }

        [Display(Name = "Description")]
        [MaxLength(400)]
        public string Description { get; set; }

        [Display(Name = "AccessLevel")]
        public UserAccessLevel AccessLevel { get; set; }

        [Display(Name = "Phone")]
        [MaxLength(50)]
        public string Phone { get; set; }

        [Display(Name = "State")]
        public byte? State { get; set; }

        [Display(Name = "ActivationCode")]
        [MaxLength(20)]
        public string ActivationCode { get; set; }

        [Display(Name = "LastRequestActivationCode")]
        [Column(TypeName = "datetime")]
        [DataType(DataType.Date)]
        public DateTime? LastRequestActivationCode { get; set; }

        public byte? ValidMobile { get; set; }

        public byte? ValidEmail { get; set; }

        [Display(Name = "RegisterDate")]
        [Column(TypeName = "datetime")]
        [DataType(DataType.Date)]
        public DateTime? RegisterDate { get; set; }

        [Display(Name = "LastLoginIp")]
        [MaxLength(20)]
        public string LastLoginIp { get; set; }

        [Display(Name = "LastLoginDate")]
        [Column(TypeName = "datetime")]
        [DataType(DataType.Date)]
        public DateTime? LastLoginDate { get; set; }

        [Display(Name = "ExpireDate")]
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

        [Display(Name = "Addrress")]
        [MaxLength(200)]
        public string Addrress { set; get; }
        
        [Display(Name = "Position")]
        [MaxLength(30)]
        public string Position { set; get; }

        [Required(ErrorMessage = "PublicRequireValidation")]
        [Display(Name = "City")]
        [MaxLength(50)]
        public string City { set; get; }

        public string SecuritySpan { set; get; }

        [Column(TypeName = "datetime")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        public EducationLevel? Degree { get; set; }

        public decimal? Sharj { get; set; }

        public ICollection<ClassRoomDetails> ClassRoomDetails { set; get; }

        public ICollection<Section> Sections { set; get; }
    }


}
