using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;

namespace OnlineCourse.Core.Extentions
{
    public class RtlCheck
    {
        IHttpContextAccessor _HttpContextAccessor;
        public RtlCheck(IHttpContextAccessor HttpContextAccessor)
        {
            _HttpContextAccessor = HttpContextAccessor;
        }
        public bool IsRtl()
        {
            bool res = false;

            var currentCulture = _HttpContextAccessor.HttpContext.Request.Cookies[CookieRequestCultureProvider.DefaultCookieName];
            if (string.IsNullOrEmpty(currentCulture))
            {
                _HttpContextAccessor.HttpContext.Response.Cookies.Append(
                                CookieRequestCultureProvider.DefaultCookieName,
                                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("en-US")),
                                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            }
            else
            {
                var cc = currentCulture.Substring(2, 5);
                if (cc == "fa-IR")
                {
                    res = true;
                }
                else if (cc == "en-US")
                {
                    res = false;
                }
            }



            return res;
        }
    }
}
