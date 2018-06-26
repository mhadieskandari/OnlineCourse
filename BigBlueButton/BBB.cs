using System;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using BigBlueButton.Models.WebHook;

namespace BigBlueButton
{
    public class BBB
    {
        private readonly Log _log;
        private string _serverIpAddress;
        private string _serverId;

        public BBB(string serverIpAddress, string serverId)
        {
            this._serverIpAddress = serverIpAddress;
            this._serverId = serverId;
            _log = new Log(AppDomain.CurrentDomain.BaseDirectory + "log.txt", 1000);
        }

        public bool WebHookCheckSumIsOk(string eventt, string callBackUrl, string checkSum)
        {
            var concatenation = callBackUrl + eventt + GetSalt();
            var sh1Concat = Sha1.GetSha1(concatenation);
            return sh1Concat.Equals(checkSum);
        }

        #region "CreateMeeting"      

        /// <summary>
        /// Creates the Meeting
        /// </summary>
        /// <param name="meetingName">Creates the Meeting with the Specified MeetingName</param>
        /// <param name="meetingId">Creates the Meeting with the Specified MeetingId</param>
        /// <param name="attendeePw">Creates the Meeting with the Specified AttendeeePassword</param>
        /// <param name="moderatorPw">Creates the Meeting with the Specified ModeratorPassword</param>
        /// <param name="logoutUrl"></param>
        /// <param name="welcome"></param>
        /// <returns></returns>
        public DataTable CreateMeeting(string meetingName, string meetingId, string attendeePw, string moderatorPw, string logoutUrl, string welcome, bool record = false)
        {
            try
            {
                var strServerIpAddress = GetServerIpAddress();//_serverIpAddress;
                var strSalt = GetSalt();//_serverId;
                var strParameters = "allowStartStopRecording=false" +
                    "&attendeePW=" + WebUtility.UrlEncode(attendeePw) +
                    "&autoStartRecording=false";
                //var strParameters= "allowStartStopRecording=false&attendeePW=ap&autoStartRecording=false&meetingID=%D8%AA%D8%B3%D8%AA+%D8%AB%D8%A8%D8%AA+%D9%86%D8%A7%D9%85&moderatorPW=mp&name=%D8%AA%D8%B3%D8%AA+%D8%AB%D8%A8%D8%AA+%D9%86%D8%A7%D9%85&record=false";
                //var strParameters= "allowStartStopRecording=false&attendeePW=ap&autoStartRecording=false&meetingID=%D8%AA%D8%B3%D8%AA+%D9%81%D8%A7%D8%B5%D9%84%D9%87&moderatorPW=mp&name=%D8%AA%D8%B3%D8%AA+%D9%81%D8%A7%D8%B5%D9%84%D9%87&record=false";
                if (!string.IsNullOrEmpty(logoutUrl))
                {
                    strParameters += "&logoutURL=" + logoutUrl;
                }
                strParameters += "&meetingID=" + WebUtility.UrlEncode(meetingId) +
                    "&moderatorPW=" + WebUtility.UrlEncode(moderatorPw) +
                    "&name=" + WebUtility.UrlEncode(meetingName);
                strParameters += "&record=" + record.ToString().ToLower();
                if (!string.IsNullOrEmpty(welcome))
                {
                    strParameters += "&welcome=" + WebUtility.UrlEncode(welcome);
                }

                //var strParameters = "meetingID=%D8%B1%DB%8C%D8%A7%D8%B6%DB%8C+%D8%A7%D9%88%D9%84&moderatorPW=Qq12345678%2540&name=%D8%B1%DB%8C%D8%A7%D8%B6%DB%8C+%D9%BE%D8%A7%DB%8C%D9%87+%D8%A7%D9%88%D9%84";

                var concatenate = "create" + strParameters + strSalt;
                var strSha1CheckSum = Sha1.GetSha1(concatenate);
                var url = "http://" + strServerIpAddress + "/bigbluebutton/api/create?" + strParameters + "&checksum=" + strSha1CheckSum;
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/x-www-form-urlencoded";
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    var tt = response.ContentType;
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        var ds = new DataSet("DataSet1");
                        ds.ReadXml(sr);
                        return ds.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Write(ex.Message);
                return null;
            }
        }

        public DataTable CreateHooks(string callbackUrl/*,string meetingId*/)
        {
            try
            {
                var strServerIpAddress = GetServerIpAddress();//_serverIpAddress;
                var strSalt = GetSalt(); //_serverId;
                var strParameters = "callbackURL=" + callbackUrl;//+"&meetingid="+meetingId ;
                var strSha1CheckSum = Sha1.GetSha1("hooks/create" + strParameters + strSalt);
                //var res = "http://" + strServerIpAddress + "/bigbluebutton/api/hooks/create?" + strParameters +
                //          "&checksum=" + strSha1CheckSum;
                //return res;
                var request = (HttpWebRequest)WebRequest.Create("http://" + strServerIpAddress + "/bigbluebutton/api/hooks/create?" + strParameters + "&checksum=" + strSha1CheckSum);
                var response = (HttpWebResponse)request.GetResponse();
                var sr = new StreamReader(response.GetResponseStream());
                var ds = new DataSet("DataSet1");
                ds.ReadXml(sr);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                _log.Write(ex.Message);
                return null;
            }
        }
        public string HooksList()
        {
            try
            {
                var strServerIpAddress = GetServerIpAddress();// _serverIpAddress;
                var strSalt = GetSalt();//_serverId;
                var strSha1CheckSum = Sha1.GetSha1("hooks/list" + strSalt);
                var res = "http://" + strServerIpAddress + "/bigbluebutton/api/hooks/list?" + "checksum=" + strSha1CheckSum;
                return res;
                //var request = (HttpWebRequest)WebRequest.Create("http://" + strServerIpAddress + "/bigbluebutton/api/hooks/create?" + strParameters + "&checksum=" + strSha1CheckSum);
                //var response = (HttpWebResponse)request.GetResponse();
                //var sr = new StreamReader(response.GetResponseStream());
                //var ds = new DataSet("DataSet1");
                //ds.ReadXml(sr);
                //return ds.Tables[0];
            }
            catch (Exception ex)
            {
                _log.Write(ex.Message);
                return null;
            }
        }

        public string CreateMeeting1(string meetingName, string meetingId, string attendeePw, string moderatorPw)
        {
            try
            {
                var strServerIpAddress = _serverIpAddress;
                var strSalt = _serverId;
                var strParameters = "name=" + WebUtility.UrlEncode(meetingName) + "&meetingID=" + WebUtility.UrlEncode(meetingId) + "&attendeePW=" + WebUtility.UrlEncode(attendeePw) + "&moderatorPW=" + WebUtility.UrlEncode(moderatorPw);
                var strSha1CheckSum = Sha1.GetSha1("create" + strParameters + strSalt);
                var request = "http://" + strServerIpAddress + "/bigbluebutton/api/create?" + strParameters + "&checksum=" + strSha1CheckSum;
                //var response = (HttpWebResponse)request.GetResponse();
                //var sr = new StreamReader(response.GetResponseStream());
                //var ds = new DataSet("DataSet1");
                //ds.ReadXml(sr);
                //return ds.Tables[0];
                return request;
            }
            catch (Exception ex)
            {
                _log.Write(ex.Message);
                return null;
            }
        }
        #endregion

        /// <summary>
        /// This project is developed by Godwin from GloriaTech.com.
        /// Contact me support in integrating BigBlueButton with .Net or for customizing BigBlueButton in anyway.
        /// E-mail me at godwin@gloriatech.com for any support on this code or even support on BigBlueButton.
        /// </summary>

        #region "JoinMeeting"
        /// <summary>
        /// To Join in the Existing Meeting
        /// </summary>
        /// <param name="meetingName">To Join in the ExistingMeeting with the Specified MeetingName</param>
        /// <param name="meetingId">To Join in the ExistingMeeting with the Specified MeetingId</param>
        /// <param name="password">To Join in the ExistingMeeting with the Specified ModeratorPW/AttendeePW</param>
        /// <param name="showInBrowser">If its true,will Show the Meeting UI in the Browser </param>
        /// <returns></returns>
        public string JoinMeeting(string meetingName, string meetingId, string password, bool showInBrowser)
        {
            try
            {
                var strServerIpAddress = GetServerIpAddress();// _serverIpAddress;
                var strSalt = GetSalt(); //_serverId;
                var strParameters = "fullName=" + WebUtility.UrlEncode(meetingName) + "&meetingID=" + WebUtility.UrlEncode(meetingId) + "&password=" + WebUtility.UrlEncode(password);
                var strSha1CheckSum = Sha1.GetSha1("join" + strParameters + strSalt);
                if (!showInBrowser)
                {
                    var request = (HttpWebRequest)WebRequest.Create("http://" + strServerIpAddress + "/bigbluebutton/api/join?" + strParameters + "&checksum=" + strSha1CheckSum);
                    var response = (HttpWebResponse)request.GetResponse();
                    var sr = new StreamReader(response.GetResponseStream());
                    return sr.ReadToEnd();
                }
                return "http://" + strServerIpAddress + "/bigbluebutton/api/join?" + strParameters + "&checksum=" + strSha1CheckSum;
            }
            catch (Exception ex)
            {
                _log.Write(ex.Message);
                return null;
            }
        }
        #endregion

        /// <summary>
        /// This project is developed by Godwin from GloriaTech.com.
        /// Contact me support in integrating BigBlueButton with .Net or for customizing BigBlueButton in anyway.
        /// E-mail me at godwin@gloriatech.com for any support on this code or even support on BigBlueButton.
        /// </summary>

        #region "IsMeetingRunning"
        /// <summary>
        /// To find the Status of the Existing Meeting
        /// </summary>
        /// <param name="meetingId">To find the Status of the Existing Meeting with the Specified MeetingId</param>
        /// <returns></returns>
        public DataTable IsMeetingRunning(string meetingId)
        {
            try
            {
                var strServerIpAddress = _serverIpAddress; //_serverIpAddress;
                var strSalt = _serverId; //_serverId;
                var strParameters = "meetingID=" + meetingId;
                var strSha1CheckSum = Sha1.GetSha1("isMeetingRunning" + strParameters + strSalt);
                var request = (HttpWebRequest)WebRequest.Create("http://" + strServerIpAddress + "/bigbluebutton/api/isMeetingRunning?" + strParameters + "&checksum=" + strSha1CheckSum);
                var response = (HttpWebResponse)request.GetResponse();
                var sr = new StreamReader(response.GetResponseStream());
                var ds = new DataSet("DataSet1");
                ds.ReadXml(sr);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                _log.Write(ex.Message);
                return null;
            }
        }
        #endregion

        /// <summary>
        /// This project is developed by Godwin from GloriaTech.com.
        /// Contact me support in integrating BigBlueButton with .Net or for customizing BigBlueButton in anyway.
        /// E-mail me at godwin@gloriatech.com for any support on this code or even for any support on BigBlueButton.
        /// </summary>

        #region "GetMeetingInfo"
        /// <summary>
        /// To Get the relavant information about the Meeting
        /// </summary>
        /// <param name="meetingId">To Get the relavant information about the Meeting with the Specified MeetingId</param>
        /// <param name="moderatorPassword">To Get the relavant information about the Meeting with the Specified ModeratorPW</param>
        /// <returns></returns>
        public DataTable GetMeetingInfo(string meetingId, string moderatorPassword)
        {
            try
            {
                var strServerIpAddress = _serverIpAddress;
                var strSalt = _serverId;
                var strParameters = "meetingID=" + meetingId + "&password=" + moderatorPassword;
                var strSha1CheckSum = Sha1.GetSha1("getMeetingInfo" + strParameters + strSalt);
                var request = (HttpWebRequest)WebRequest.Create("http://" + strServerIpAddress + "/bigbluebutton/api/getMeetingInfo?" + strParameters + "&checksum=" + strSha1CheckSum);
                var response = (HttpWebResponse)request.GetResponse();
                var sr = new StreamReader(response.GetResponseStream());
                var ds = new DataSet("DataSet1");
                ds.ReadXml(sr);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                _log.Write(ex.Message);
                return null;
            }
        }
        #endregion

        /// <summary>
        /// This project is developed by Godwin from GloriaTech.com.
        /// Contact me support in integrating BigBlueButton with .Net or for customizing BigBlueButton in anyway.
        /// E-mail me at godwin@gloriatech.com for any support on this code or even any support on BigBlueButton.
        /// </summary>

        #region "EndMeeting"
        /// <summary>
        /// To End the Meeting
        /// </summary>
        /// <param name="meetingId">To End the Meeting with the Specified MeetingId</param>
        /// <param name="moderatorPassword">To End the Meeting with the Specified ModeratorPW</param>
        /// <returns></returns>
        public DataTable EndMeeting(string meetingId, string moderatorPassword)
        {
            try
            {
                var strServerIpAddress = _serverIpAddress;
                var strSalt = _serverId;
                var strParameters = "meetingID=" + meetingId + "&password=" + moderatorPassword;
                var strSha1CheckSum = Sha1.GetSha1("end" + strParameters + strSalt);
                var request = (HttpWebRequest)WebRequest.Create("http://" + strServerIpAddress + "/bigbluebutton/api/end?" + strParameters + "&checksum=" + strSha1CheckSum);
                var response = (HttpWebResponse)request.GetResponse();
                var sr = new StreamReader(response.GetResponseStream());
                var ds = new DataSet("DataSet1");
                ds.ReadXml(sr);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                _log.Write(ex.Message);
                return null;
            }
        }
        #endregion

        /// <summary>
        /// This project is developed by Godwin from GloriaTech.com.
        /// Contact me support in integrating BigBlueButton with .Net or for customizing BigBlueButton in anyway.
        /// E-mail me at godwin@gloriatech.com for any support on this code or even for any support on BigBlueButton.
        /// </summary>

        #region "getMeetings"
        /// <summary>
        /// To Get all the Meeting's Information running in the Server
        /// </summary>
        /// <returns></returns>
        public DataTable getMeetings()
        {
            try
            {
                var strServerIpAddress = _serverIpAddress;
                var strSalt = _serverId;
                var r = new Random(0);
                var strParameters = "random=" + r.Next(100).ToString();
                var strSha1CheckSum = Sha1.GetSha1("getMeetings" + strParameters + strSalt);
                var request = (HttpWebRequest)WebRequest.Create("http://" + strServerIpAddress + "/bigbluebutton/api/getMeetings?" + strParameters + "&checksum=" + strSha1CheckSum);
                var response = (HttpWebResponse)request.GetResponse();
                var sr = new StreamReader(response.GetResponseStream());
                var ds = new DataSet("DataSet1");
                ds.ReadXml(sr);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                _log.Write(ex.Message);
                return null;
            }
        }
        #endregion


        public string GetSalt()
        {
            return _serverId;
        }

        public string GetServerIpAddress()
        {
            return _serverIpAddress;
        }


    }
}
