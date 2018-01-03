using System;

namespace OnlineCourse.Core.Dtos
{
    public class UserDto
    {
        public int Id { set; get; }
       
        public string UserName { set; get; }
        
        public string Email { set; get; }
        
        public string FullName { set; get; }
        
        public string Pwd { get; set; }

        public string Mobile { get; set; }

        public string Des { get; set; }

        public byte? AccessLevel { get; set; }

        public string Phone { get; set; }

        public byte? State { get; set; }

        public string ActivationCode { get; set; }

        public DateTime? LastRequestActivationCode { get; set; }

        public byte? ValidMobile { get; set; }

        public byte? ValidEmail { get; set; }

        public DateTime? RegisterDate { get; set; }

        public string LastLoginIp { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public DateTime? ExpireDate { get; set; }

        public string MobileCreditAlarm { get; set; }

        public int? LastVisitedNewsId { get; set; }

        public int? RegisterAttemptFailure { get; set; }

        public int? LoginAttemptFailure { get; set; }

        public DateTime? LastResetPwdDate { get; set; }

        public string Addrress { set; get; }

        public int? ThumbnailId { set; get; }

        public string Position { set; get; }

        public string WorkDays { set; get; }

        public string City { set; get; }

        public string Country { set; get; }

        public byte? OnOff { set; get; }

        public string SecuritySpan { set; get; }

        public string ShowName { set; get; }
    }
}
