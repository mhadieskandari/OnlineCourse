using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using BigBlueButton;
using BigBlueButton.Models.WebHook;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Internal.System.Text.Encodings.Web.Utf8;
using Newtonsoft.Json;
using OnlineCourse.Core;
using OnlineCourse.Core.Services;
using OnlineCourse.Entity;
using OnlineCourse.Panel.Utils.Extentions;
using OnlineCourse.Panel.Utils.ViewModels;

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
                var settings = new JsonSerializerSettings()
                {
                    ContractResolver = new OrderedContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore,
                    
                };
                var eventt = "event=" + JsonConvert.SerializeObject(eventModel, Formatting.None, settings) + "&timestamp=" + req["timestamp"];
                eventt = WebUtility.UrlEncode(eventt);
                //var eventt = "event=" + req["event"]+ "&timestamp=" + req["timestamp"];
                var request = _httpContextAccessor.HttpContext.Request;
                var uriBuilder = new UriBuilder
                {
                    Scheme = request.Scheme,
                    Host = request.Host.Host,
                    Path = "api/BigBlueButtonHooks/index",
                    Query = "meetingid=" + meetingid
                };
                if (request.Host.Port != null)
                {
                    uriBuilder.Port = request.Host.Port.Value;
                }
                var bbb = new BBB(_config.BbbGetServerIpAddress(), _config.BbbGetServerId());
                if (bbb.WebHookCheckSumIsOk(eventt, uriBuilder.ToString(), checksum))
                {
                    var a = "test";
                }
                var meeting = _context.ClassRooms.SingleOrDefault(c => c.Id == meetingid.Value);
                if (meeting == null)
                    return Json("there is not any meeting.");
                switch (eventModel.header.name)
                {
                    case WebHookEvents.meeting_created_message:
                        meeting.Status = ClassStatus.OnGoing;
                        _context.SaveChanges();
                        break;
                    case WebHookEvents.meeting_destroyed_event:
                        meeting.Status = ClassStatus.Complete;
                        _context.SaveChanges();
                        break;
                    case WebHookEvents.user_joined_message:
                        meeting.Status = ClassStatus.OnGoing;
                        _context.SaveChanges();
                        break;
                    case WebHookEvents.user_left_message:
                        if (eventModel.payload.user.presenter)
                        {
                            meeting.Status = ClassStatus.Complete;
                            _context.SaveChanges();
                        }
                        break;
                    case WebHookEvents.archive_started:
                        break;
                    case WebHookEvents.archive_ended:
                        break;
                    case WebHookEvents.process_ended:
                        break;
                    case WebHookEvents.process_started:
                        break;
                    case WebHookEvents.post_archive_started:
                        break;
                    case WebHookEvents.post_publish_started:
                        break;
                    case WebHookEvents.post_publish_ended:
                        break;
                    case WebHookEvents.post_archive_ended:
                        break;
                    case WebHookEvents.publish_started:
                        break;
                    case WebHookEvents.publish_ended:
                        break;
                    case WebHookEvents.post_process_ended:
                        break;
                    case WebHookEvents.post_process_started:
                        break;
                    case WebHookEvents.sanity_ended:
                        break;
                    case WebHookEvents.sanity_started:
                        break;
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