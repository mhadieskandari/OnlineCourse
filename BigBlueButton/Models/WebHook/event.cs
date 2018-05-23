using System;
using System.Collections.Generic;
using System.Text;

namespace BigBlueButton.Models.WebHook
{
    //{[event, {"payload":{"meeting_id":"e6c3dd630428fd54834172b8fd2735fed9416da4-1527010228485","user":{"role":"MODERATOR","presenter":true,"locked":false,"extern_userid":"ortnjtvmvjg5","phone_user":false,"webcam_stream":[],"emoji_status":"none","voiceUser":{"talking":false,"callername":"teacher@gmail.com","locked":false,"callernum":"teacher@gmail.com","muted":false,"web_userid":"ortnjtvmvjg5_1","joined":false,"userid":"ortnjtvmvjg5_1"},"name":"teacher@gmail.com","listenOnly":false,"avatarURL":"http://192.168.149.132/client/avatar.png","userid":"ortnjtvmvjg5_1","has_stream":false}},"header":{"name":"user_left_message","version":"0.0.1","current_time":1527010593930,"timestamp":18738556}}]}
    //"name":"teacher@gmail.com","listenOnly":false,"avatarURL":"http://192.168.149.132/client/avatar.png","userid":"ortnjtvmvjg5_1","has_stream":false}
    public class Event
    {
        public Header header { set; get; }
        public Payload payload { set; get; }
    }

    public class Header
    {
        public string timestamp { set; get; }
        public string name { set; get; }
        public string current_time { set; get; }
        public string version { set; get; }
    }

    public class Payload
    {
        public string meeting_id { set; get; }

        public User user { set; get; }
    }

    public class User
    {
        public string role { set; get; }
        public bool presenter { set; get; }
        public bool locked { set; get; }
        public bool phone_user { set; get; }
        public string extern_userid { set; get; }
        public string emoji_status { set; get; }
        //public string webcam_stream { set; get; } "webcam_stream":[]
        public VoiceUser voiceuser { set; get; }
        public string name { set; get; }
        public bool listenOnly { set; get; }
        public string avatarURL { set; get; }
        public string userid { set; get; }
        public bool has_stream { set; get; }
    }

    public class VoiceUser
    {
        public bool talking { set; get; }
        public string callername { set; get; }
        public bool locked { set; get; }
        public string callernum { set; get; }
        public bool muted { set; get; }
        public string web_userid { set; get; }
        public bool joined { set; get; }
        public string userid { set; get; }

    }
}
