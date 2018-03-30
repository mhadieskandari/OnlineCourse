using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using OnlineCourse.Core.Repositories.Interfaces;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using OnlineCourse.Core.Services;

namespace OnlineCourse.Core.Extentions
{
    public class LastAuthChangedValidator: CookieAuthenticationEvents
    {        
        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            try
            {
                var unitOfWork = context.HttpContext.RequestServices.GetRequiredService<IUnitOfWork>();
                var user = context.HttpContext.RequestServices.GetRequiredService<CurrentUser>();

                var userPrincipal = context.Principal;
                var lastChanged = (from c in userPrincipal.Claims where c.Type == ClaimTypes.SerialNumber select c.Value).FirstOrDefault();
                var username = userPrincipal.Claims.FirstOrDefault(m => m.Type == ClaimTypes.Email);
                //var userId = _cuser.GetUserId();

                if (username != null)
                {
                    if (string.IsNullOrEmpty(lastChanged) || !await unitOfWork.Users.ValidateLastChanged(username.Value, lastChanged))
                    {
                        //context.RejectPrincipal();
                        //await context.HttpContext.SignOutAsync("onlineCourseAthentication");
                        await user.LogOutAsync();
                    }
                }
                else
                {
                    //context.RejectPrincipal();
                    //await context.HttpContext.SignOutAsync("onlineCourseAthentication");

                    await user.LogOutAsync();
                }
            }
            catch (Exception e)
            {
                //var httpContextAccessor =context.HttpContext.RequestServices.GetRequiredService<IHttpContextAccessor>();
                //var hostingEnvironment= context.HttpContext.RequestServices.GetRequiredService<IHostingEnvironment>();
                //var serviceProvider = context.HttpContext.RequestServices.GetRequiredService<IServiceProvider>();
                //var historyService=new HistoryService(serviceProvider,httpContextAccessor,hostingEnvironment);
                //historyService.LogError(e,CHistoryErrorType.Middle);
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
