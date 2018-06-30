using System.Collections.Generic;

namespace BigBlueButton.Models.WebHook
{
    //{[event, {"payload":{"meeting_id":"e6c3dd630428fd54834172b8fd2735fed9416da4-1527010228485","user":{"role":"MODERATOR","presenter":true,"locked":false,"extern_userid":"ortnjtvmvjg5","phone_user":false,"webcam_stream":[],"emoji_status":"none","voiceUser":{"talking":false,"callername":"teacher@gmail.com","locked":false,"callernum":"teacher@gmail.com","muted":false,"web_userid":"ortnjtvmvjg5_1","joined":false,"userid":"ortnjtvmvjg5_1"},"name":"teacher@gmail.com","listenOnly":false,"avatarURL":"http://192.168.149.132/client/avatar.png","userid":"ortnjtvmvjg5_1","has_stream":false}},"header":{"name":"user_left_message","version":"0.0.1","current_time":1527010593930,"timestamp":18738556}}]}
    //"name":"teacher@gmail.com","listenOnly":false,"avatarURL":"http://192.168.149.132/client/avatar.png","userid":"ortnjtvmvjg5_1","has_stream":false}
    public class Event
    {
        public Payload payload { get; set; }
        public Header header { get; set; }
    }

    public class Header
    {
        public string name { get; set; }
        public string version { get; set; }
        public long current_time { get; set; }
        public int timestamp { get; set; }
    }

    public class Payload
    {
        public int duration { get; set; }
        public string external_meeting_id { get; set; }
        public long create_time { get; set; }
        public string meeting_id { get; set; }
        public bool is_breakout { get; set; }
        public string name { get; set; }
        public string moderator_pass { get; set; }
        public bool recorded { get; set; }
        public string voice_conf { get; set; }
        public string viewer_pass { get; set; }
        public string create_date { get; set; }
        public User user { set; get; }
    }

    public class User
    {
        public string role { get; set; }
        public bool presenter { get; set; }
        public bool locked { get; set; }
        public string extern_userid { get; set; }
        public bool phone_user { get; set; }
        public List<object> webcam_stream { get; set; }
        public string emoji_status { get; set; }
        public VoiceUser voiceUser { get; set; }
        public string name { get; set; }
        public bool listenOnly { get; set; }
        public string avatarURL { get; set; }
        public string userid { get; set; }
        public bool has_stream { get; set; }
    }
    public class VoiceUser
    {
        public bool talking { get; set; }
        public string callername { get; set; }
        public bool locked { get; set; }
        public string callernum { get; set; }
        public bool muted { get; set; }
        public string web_userid { get; set; }
        public bool joined { get; set; }
        public string userid { get; set; }
    }
}
