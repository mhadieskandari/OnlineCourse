using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BigBlueButton.Models.WebHook
{
    //{[event, {"payload":{"meeting_id":"e6c3dd630428fd54834172b8fd2735fed9416da4-1527010228485","user":{"role":"MODERATOR","presenter":true,"locked":false,"extern_userid":"ortnjtvmvjg5","phone_user":false,"webcam_stream":[],"emoji_status":"none","voiceUser":{"talking":false,"callername":"teacher@gmail.com","locked":false,"callernum":"teacher@gmail.com","muted":false,"web_userid":"ortnjtvmvjg5_1","joined":false,"userid":"ortnjtvmvjg5_1"},"name":"teacher@gmail.com","listenOnly":false,"avatarURL":"http://192.168.149.132/client/avatar.png","userid":"ortnjtvmvjg5_1","has_stream":false}},"header":{"name":"user_left_message","version":"0.0.1","current_time":1527010593930,"timestamp":18738556}}]}
    //"name":"teacher@gmail.com","listenOnly":false,"avatarURL":"http://192.168.149.132/client/avatar.png","userid":"ortnjtvmvjg5_1","has_stream":false}
    public class Event
    {
        [JsonProperty]
        public Header header { set; get; }
        [JsonProperty]
        public Payload payload { set; get; }
    }

    public class Header
    {
        [JsonProperty]
        public string timestamp { set; get; }
        [JsonProperty]
        public string name { set; get; }
        [JsonProperty]
        public string current_time { set; get; }
        [JsonProperty]
        public string version { set; get; }
    }

    public class Payload
    {
        [JsonProperty]
        public string meeting_id { set; get; }
        [JsonProperty]
        public User user { set; get; }
    }

    public class User
    {
        [JsonProperty]
        public string role { set; get; }
        [JsonProperty]
        public bool presenter { set; get; }
        [JsonProperty]
        public bool locked { set; get; }
        [JsonProperty]
        public bool phone_user { set; get; }
        [JsonProperty]
        public string extern_userid { set; get; }
        [JsonProperty]
        public string emoji_status { set; get; }
        [JsonProperty]
        public string[] webcam_stream { set; get; } //"webcam_stream":[]
        [JsonProperty]
        public VoiceUser voiceuser { set; get; }
        [JsonProperty]
        public string name { set; get; }
        [JsonProperty]
        public bool listenOnly { set; get; }
        [JsonProperty]
        public string avatarURL { set; get; }
        [JsonProperty]
        public string userid { set; get; }
        [JsonProperty]
        public bool has_stream { set; get; }

    }

    public class VoiceUser
    {
        [JsonProperty]
        public bool talking { set; get; }
        [JsonProperty]
        public string callername { set; get; }
        [JsonProperty]
        public bool locked { set; get; }
        [JsonProperty]
        public string callernum { set; get; }
        [JsonProperty]
        public bool muted { set; get; }
        [JsonProperty]
        public string web_userid { set; get; }
        [JsonProperty]
        public bool joined { set; get; }
        [JsonProperty]
        public string userid { set; get; }

    }
}
