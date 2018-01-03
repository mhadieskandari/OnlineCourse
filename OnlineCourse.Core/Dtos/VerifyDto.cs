namespace OnlineCourse.Core.Dtos
{
    public class VerifyDto
    {
        public string Email { set; get; }
        public string VerificationCode { set; get; }
        public string Ip { set; get; }
    }
}
