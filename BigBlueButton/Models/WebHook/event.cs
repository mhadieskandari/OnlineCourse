using System;
using System.Collections.Generic;
using System.Text;

namespace BigBlueButton.Models.WebHook
{
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
    }
}
