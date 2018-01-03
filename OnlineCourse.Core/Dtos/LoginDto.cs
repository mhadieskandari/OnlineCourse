namespace OnlineCourse.Core.Dtos
{
    public class LoginDto
    {
        public string Email { set; get; }
        public string PassWord { set; get; }
        public string Ip { set; get; }
        public bool? Remember { set; get; }
    }
}