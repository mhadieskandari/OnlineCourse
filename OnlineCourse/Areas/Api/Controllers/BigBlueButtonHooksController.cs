using System;
using System.Linq;
using AutoMapper;
using BigBlueButton.Models.WebHook;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineCourse.Core;
using OnlineCourse.Core.Services;
using OnlineCourse.Entity;
using OnlineCourse.Panel.Utils.Extentions;

namespace OnlineCourse.Panel.Areas.Api.Controllers
{
    [Area("Api")]
    public class BigBlueButtonHooksController : BaseController
    {
        public BigBlueButtonHooksController(ApplicationDbContext context, CurrentUser user, HistoryService history, IServiceProvider provider, IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, IMapper mapper, PublicConfig config) 
            : base(context, user, history, provider, hostingEnvironment, httpContextAccessor, mapper, config)
        {
        }
        public IActionResult Index(int? meetingid, string checksum)
        {
            try
            {

                var req = _httpContextAccessor.HttpContext.Request.Form;
                var eventModel = JsonConvert.DeserializeObject<Event>(req["event"]);
                //var settings = new JsonSerializerSettings()
                //{
                //    ContractResolver = new OrderedContractResolver(),
                //    NullValueHandling = NullValueHandling.Ignore,
                    
                    
                //};
                //var eventt = "event=" + JsonConvert.SerializeObject(eventModel, Formatting.None, settings) + "&timestamp=" + req["timestamp"];
                //eventt = WebUtility.UrlEncode(eventt);
                //var eventt = "event=" + req["event"]+ "&timestamp=" + req["timestamp"];
                //var request = _httpContextAccessor.HttpContext.Request;
                //var uriBuilder = new UriBuilder
                //{
                //    Scheme = request.Scheme,
                //    Host = request.Host.Host,
                //    Path = "api/BigBlueButtonHooks/index",
                //    Query = "meetingid=" + meetingid
                //};
                //if (request.Host.Port != null)
                //{
                //    uriBuilder.Port = request.Host.Port.Value;
                //}

                //todo checksum of webhook callback must be create . 
                //var bbb = new BBB(_config.BbbGetServerIpAddress(), _config.BbbGetServerId());
                //if (bbb.WebHookCheckSumIsOk(eventt, uriBuilder.ToString(), checksum))
                //{
                //    var a = "test";
                //}
                var meeting = _context.ClassRooms.SingleOrDefault(c => c.Id == meetingid.Value);
                if (meeting == null)
                    return Json("there is not any meeting.");
                if (eventModel.header.name == WebHookEvents.meeting_created_message)
                {
                    meeting.Status = ClassStatus.OnGoing;
                    _context.SaveChanges();
                }
                else if (eventModel.header.name == WebHookEvents.meeting_destroyed_event)
                {
                    meeting.Status = ClassStatus.Complete;
                    _context.SaveChanges();
                }
                else if (eventModel.header.name == WebHookEvents.user_joined_message)
                {
                    meeting.Status = ClassStatus.OnGoing;
                    _context.SaveChanges();
                }
                else if (eventModel.header.name == WebHookEvents.user_left_message)
                {
                    if (eventModel.payload.user.presenter)
                    {
                        meeting.Status = ClassStatus.Complete;
                        _context.SaveChanges();
                    }
                }
                else if (eventModel.header.name == WebHookEvents.archive_started)
                {
                }
                else if (eventModel.header.name == WebHookEvents.archive_ended)
                {
                }
                else if (eventModel.header.name == WebHookEvents.process_ended)
                {
                }
                else if (eventModel.header.name == WebHookEvents.process_started)
                {
                }
                else if (eventModel.header.name == WebHookEvents.post_archive_started)
                {
                }
                else if (eventModel.header.name == WebHookEvents.post_publish_started)
                {
                }
                else if (eventModel.header.name == WebHookEvents.post_publish_ended)
                {
                }
                else if (eventModel.header.name == WebHookEvents.post_archive_ended)
                {
                }
                else if (eventModel.header.name == WebHookEvents.publish_started)
                {
                }
                else if (eventModel.header.name == WebHookEvents.publish_ended)
                {
                }
                else if (eventModel.header.name == WebHookEvents.post_process_ended)
                {
                }
                else if (eventModel.header.name == WebHookEvents.post_process_started)
                {
                }
                else if (eventModel.header.name == WebHookEvents.sanity_ended)
                {
                }
                else if (eventModel.header.name == WebHookEvents.sanity_started)
                {
                }
                return Json("ok");
            }
            catch (Exception e)
            {
                _history.LogError(e, HistoryErrorType.Middle);
                return Json("error");
            }
        }
    }
}