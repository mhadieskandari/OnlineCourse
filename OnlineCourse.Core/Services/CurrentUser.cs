using OnlineCourse.Entity.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using OnlineCourse.Entity;
using System;
using OnlineCourse.Core.Extentions;

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

        public async Task<EducationLevel> GetUserDegree()
        {

            var eLevel = (await _unitOfWork.Users.GetByEmailAsync(GetEmail())).Degree;
            return eLevel.Value;
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

            //userIdentity.AddClaim(remember
            //    ? new Claim(ClaimTypes.Expiration, TimeSpan.FromDays(30).ToString())
            //    : new Claim(ClaimTypes.Expiration, "0"));

           // var expireDate = remember ? 30 : 0;

            userIdentity.AddClaim(user.SecuritySpan != null
                ? new Claim(ClaimTypes.SerialNumber, user.SecuritySpan)
                : new Claim(ClaimTypes.SerialNumber, "null"));
            var ac = ((byte)user.AccessLevel).ToString();
            userIdentity.AddClaim(new Claim(ClaimTypes.Role, ac));

            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

            if (remember)
            {
                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    principal, new AuthenticationProperties()
                    {
                        ExpiresUtc = DateTimeOffset.Now.AddDays(30),
                        IsPersistent = true,

                    });
            }
            else
            {
                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal);
            }

            

            return user;
        }

        public async Task<string> GetUserProfilePicPath()
        {
            string path = "";
            var userId = await GetUserId();
            if (userId != null)
            {
                var gal = await _unitOfWork.Galleries.GetUserProfileAsync(userId.Value);
                if (gal?.Kind != null)
                    path = EncryptDecrypt.GetUrlHash(gal.Id.ToString() + gal.PublicId + gal.Kind.Value) + gal.Ext;
            }
            return path;
        }

        public async Task<string> GetUserProfilePicPath(int userId)
        {

            string path = "";
            var gal = await _unitOfWork.Galleries.GetUserProfileAsync(userId);
            if (gal?.Kind != null)
                path = EncryptDecrypt.GetUrlHash(gal.Id.ToString() + gal.PublicId + gal.Kind.Value) + gal.Ext;
            return path;
        }


    }
}
