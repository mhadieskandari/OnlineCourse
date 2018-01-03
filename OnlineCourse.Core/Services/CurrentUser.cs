using OnlineCourse.Entity.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace OnlineCourse.Core.Services
{
    public class CurrentUser
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }


        public string GetEmail()
        {
            return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(m => m.Type == ClaimTypes.Email)?.Value;
        }

        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public async Task<int?> GetUserId()
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(GetEmail());
            return user?.Id;
        }

        public async Task<User> GetUser()
        {

            var user = await _unitOfWork.Users.GetByEmailAsync(GetEmail());
            return user;
        }

        public async Task<string> GetUserName()
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(GetEmail());
            return user?.UserName;
        }

        public async Task<string> GetFullName()
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(GetEmail());
            return user?.FullName;
        }
        
      
        public async Task LogOutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }


        public async Task<User> Login(string email,string password,bool remember)
        {
            var user = _unitOfWork.Users.Get(email, password);
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, email) };

            var userIdentity = new ClaimsIdentity(claims, "login");

            if (user.Email != null) userIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));

            if (user.FullName != null) userIdentity.AddClaim(new Claim(ClaimTypes.Name, user.FullName));

            userIdentity.AddClaim(remember
                ? new Claim(ClaimTypes.Expiration, "20")
                : new Claim(ClaimTypes.Expiration, "0"));

            userIdentity.AddClaim(user.SecuritySpan != null
                ? new Claim(ClaimTypes.SerialNumber, user.SecuritySpan)
                : new Claim(ClaimTypes.SerialNumber, "null"));

            userIdentity.AddClaim(user.AccessLevel != null
                ? new Claim(ClaimTypes.Role, ((UserAccessLevel)user.AccessLevel).ToString())
                : new Claim(ClaimTypes.Role, "null"));


            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return user;
        }
        
    }
}
