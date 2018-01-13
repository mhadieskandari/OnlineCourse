
using DetectionCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using OnlineCourse.Core.Extentions;
using OnlineCourse.Entity;
using OnlineCourse.Entity.Models;
using System;

namespace OnlineCourse.Core.Services
{
    public class HistoryService : ServiceBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostingEnvironment _hostingEnvironment;
        public HistoryService(IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor, IHostingEnvironment hostingEnvironment)
            : base(serviceProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = hostingEnvironment;
        }
        public void LogError(Exception e, HistoryErrorType errorType)
        {

            using (var uw = CreateUnitOfWork())
            {


                try
                {
                    var log = new History()
                    {
                        UserId = new CurrentUser(_httpContextAccessor, uw).GetUserId().Result,
                        Os = WebHelper.GetUserAgent.Platform(),
                        Browser = WebHelper.GetUserAgent.Browser(),
                        Ip = WebHelper.GetRemoteIP,
                        Date = DateTime.Now,
                        Url = WebHelper.HttpContext.Request.GetDisplayUrl(),
                        State = (byte)HistoryState.New,
                        Message = e.ToString(),
                        Action = (byte)errorType
                    };
                    //Todo 
                    //uw.Histories.Add(log);
                    //uw.Complete();
                }
                catch (Exception exception)
                {
                    //this can be save in file      save top "log" to file
                    Console.WriteLine(exception);
                    throw;
                }
            }
        }


        //public static void LogError(ApplicationDbContext  dbContext, Exception e, CHistoryErrorType errorType)
        //{
        //    var log = new History()
        //    {
        //        UserId = new CUser(dbContext).GetUser().Id,
        //        Os = WebHelpers.GetUserAgent.Platform(),
        //        Browser = WebHelpers.GetUserAgent.Browser(),
        //        Ip= WebHelpers.GetRemoteIP,
        //        Date = DateTime.Now,
        //        Url = WebHelpers.HttpContext.Request.GetDisplayUrl(),
        //        Stat =(byte)CHistoryState.New,
        //        Message = e.Message,
        //        Action =(byte) errorType
        //    };



        //    try
        //    {
        //        dbContext.Histories.Add(log);
        //        dbContext.SaveChanges();
        //    }
        //    catch (Exception exception)
        //    {
        //        //this can be save in file      save top "log" to file
        //        Console.WriteLine(exception);
        //        throw;
        //    }
        //}




    }
}
