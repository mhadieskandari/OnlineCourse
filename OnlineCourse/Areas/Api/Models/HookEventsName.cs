using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourse.Panel.Areas.Api.Models
{
    public class HookEventsName
    {
        public static string MeetingCreatedEvtMsg = "MeetingCreatedEvtMsg";
        public static string MeetingEndedEvtMsg = "MeetingEndedEvtMsg";
        public static string UserJoinedMeetingEvtMsg = "UserJoinedMeetingEvtMsg";
        public static string UserLeftMeetingEvtMsg = "UserLeftMeetingEvtMsg";
        public static string UserJoinedVoiceConfToClientEvtMsg = "UserJoinedVoiceConfToClientEvtMsg";
        public static string UserLeftVoiceConfToClientEvtMsg = "UserLeftVoiceConfToClientEvtMsg";
        public static string UserMutedVoiceEvtMsg = "UserMutedVoiceEvtMsg";
        public static string UserBroadcastCamStartedEvtMsg = "UserBroadcastCamStartedEvtMsg";
        public static string UserBroadcastCamStoppedEvtMsg = "UserBroadcastCamStoppedEvtMsg";
        public static string RecordingStatusChangedEvtMsg = "RecordingStatusChangedEvtMsg";
        public static string sanity_started = "sanity_started";
        public static string sanity_ended = "sanity_ended";
        public static string archive_started = "archive_started";
        public static string archive_ended = "archive_ended";
        public static string post_archive_started = "post_archive_started";
        public static string post_archive_ended = "post_archive_ended";
        public static string process_started = "process_started";
        public static string process_ended = "process_ended";
        public static string post_process_started = "post_process_started";
        public static string post_process_ended = "post_process_ended";
        public static string publish_started = "publish_started";
        public static string publish_ended = "publish_ended";
        public static string post_publish_started = "post_publish_started";
        public static string post_publish_ended = "post_publish_ended";
        public static string unpublished = "unpublished";
        public static string published = "published";
        public static string deleted = "deleted";
    }
}
