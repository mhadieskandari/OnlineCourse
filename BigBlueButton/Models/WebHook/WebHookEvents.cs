using System;
using System.Collections.Generic;
using System.Text;

namespace BigBlueButton.Models.WebHook
{
    public static class WebHookEvents
    {
        public const string meeting_created_message = "meeting_created_message";
        public const string meeting_destroyed_event = "meeting_destroyed_event";
        public const string user_joined_message = "user_joined_message";
        public const string user_left_message = "user_left_message";
        public const string sanity_started = "sanity_started";
        public const string sanity_ended = "sanity_ended";
        public const string archive_started = "archive_started";
        public const string archive_ended = "archive_ended";
        public const string post_archive_started = "post_archive_started";
        public const string post_archive_ended = "post_archive_ended";
        public const string process_started = "process_started";
        public const string process_ended = "process_ended";
        public const string post_process_started = "post_process_started";
        public const string post_process_ended = "post_process_ended";
        public const string publish_started = "publish_started";
        public const string publish_ended = "publish_ended";
        public const string post_publish_started = "post_publish_started";
        public const string post_publish_ended = "post_publish_ended";



        //{ channel: "bigbluebutton:from-bbb-apps:meeting", name: "meeting_created_message" },
        //{ channel: "bigbluebutton:from-bbb-apps:meeting", name: "meeting_destroyed_event" },
        //{ channel: "bigbluebutton:from-bbb-apps:users", name: "user_joined_message" },
        //{ channel: "bigbluebutton:from-bbb-apps:users", name: "user_left_message" },
        //{ channel: "bigbluebutton:from-rap", name: "sanity_started" },
        //{ channel: "bigbluebutton:from-rap", name: "sanity_ended" },
        //{ channel: "bigbluebutton:from-rap", name: "archive_started" },
        //{ channel: "bigbluebutton:from-rap", name: "archive_ended" },
        //{ channel: "bigbluebutton:from-rap", name: "post_archive_started" },
        //{ channel: "bigbluebutton:from-rap", name: "post_archive_ended" },
        //{ channel: "bigbluebutton:from-rap", name: "process_started" },
        //{ channel: "bigbluebutton:from-rap", name: "process_ended" },
        //{ channel: "bigbluebutton:from-rap", name: "post_process_started" },
        //{ channel: "bigbluebutton:from-rap", name: "post_process_ended" },
        //{ channel: "bigbluebutton:from-rap", name: "publish_started" },
        //{ channel: "bigbluebutton:from-rap", name: "publish_ended" },
        //{ channel: "bigbluebutton:from-rap", name: "post_publish_started" },
        //{ channel: "bigbluebutton:from-rap", name: "post_publish_ended" }

        //public const string MeetingCreatedEvtMsg = "MeetingCreatedEvtMsg";
        //public const string MeetingEndedEvtMsg = "MeetingEndedEvtMsg";
        //public const string UserJoinedMeetingEvtMsg = "UserJoinedMeetingEvtMsg";
        //public const string UserLeftMeetingEvtMsg = "UserLeftMeetingEvtMsg";
        //public const string UserJoinedVoiceConfToClientEvtMsg = "UserJoinedVoiceConfToClientEvtMsg";
        //public const string UserLeftVoiceConfToClientEvtMsg = "UserLeftVoiceConfToClientEvtMsg";
        //public const string UserMutedVoiceEvtMsg = "UserMutedVoiceEvtMsg";
        //public const string UserBroadcastCamStartedEvtMsg = "UserBroadcastCamStartedEvtMsg";
        //public const string UserBroadcastCamStoppedEvtMsg = "UserBroadcastCamStoppedEvtMsg";
        //public const string RecordingStatusChangedEvtMsg = "RecordingStatusChangedEvtMsg";
        //public const string sanity_started = "sanity_started";
        //public const string sanity_ended = "sanity_ended";
        //public const string archive_started = "archive_started";
        //public const string archive_ended = "archive_ended";
        //public const string post_archive_started = "post_archive_started";
        //public const string post_archive_ended = "post_archive_ended";
        //public const string process_started = "process_started";
        //public const string process_ended = "process_ended";
        //public const string post_process_started = "post_process_started";
        //public const string post_process_ended = "post_process_ended";
        //public const string publish_started = "publish_started";
        //public const string publish_ended = "publish_ended";
        //public const string post_publish_started = "post_publish_started";
        //public const string post_publish_ended = "post_publish_ended";
        //public const string unpublished = "unpublished";
        //public const string published = "published";
        //public const string deleted = "deleted";

    }
}
