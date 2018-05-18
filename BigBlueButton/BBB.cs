using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace BigBlueButton
{
    public class BBB
    {
        private readonly Log _log;

        public BBB()
        {
            _log = new Log(AppDomain.CurrentDomain.BaseDirectory + "log.txt", 1000);
        }

       
        #region "CreateMeeting"      
        /// <summary>
        /// Creates the Meeting
        /// </summary>
        /// <param name="meetingName">Creates the Meeting with the Specified MeetingName</param>
        /// <param name="meetingId">Creates the Meeting with the Specified MeetingId</param>
        /// <param name="attendeePw">Creates the Meeting with the Specified AttendeeePassword</param>
        /// <param name="moderatorPw">Creates the Meeting with the Specified ModeratorPassword</param>
        /// <returns></returns>
        public DataTable CreateMeeting(string meetingName, string meetingId, string attendeePw, string moderatorPw)
        {
            try
            {
                var strServerIpAddress = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ServerIPAddress.txt");
                var strSalt = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ServerId.txt");
                var strParameters = "name=" + meetingName + "&meetingID=" + meetingId + "&attendeePW=" + attendeePw + "&moderatorPW=" + moderatorPw;
                var strSha1CheckSum = Sha1.GetSha1("create" + strParameters + strSalt);
                var request = (HttpWebRequest)WebRequest.Create("http://" + strServerIpAddress + "/bigbluebutton/api/create?" + strParameters + "&checksum=" + strSha1CheckSum);
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

        public string CreateHooks(string callbackUrl)
        {
            try
            {
                var strServerIpAddress = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ServerIPAddress.txt");
                var strSalt = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ServerId.txt");
                var strParameters = "callbackURL=" + callbackUrl ;
                var strSha1CheckSum = Sha1.GetSha1("hooks/create" + strParameters + strSalt);
                var res = "http://" + strServerIpAddress + "/bigbluebutton/api/hooks/create?" + strParameters +
                          "&checksum=" + strSha1CheckSum;
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
        public string HooksList()
        {
            try
            {
                var strServerIpAddress = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ServerIPAddress.txt");
                var strSalt = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ServerId.txt");
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
                var strServerIpAddress = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ServerIPAddress.txt");
                var strSalt = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ServerId.txt");
                var strParameters = "name=" + meetingName + "&meetingID=" + meetingId + "&attendeePW=" + attendeePw + "&moderatorPW=" + moderatorPw;
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
                var strServerIpAddress = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ServerIPAddress.txt");
                var strSalt = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ServerId.txt");
                var strParameters = "fullName=" + meetingName + "&meetingID=" + meetingId + "&password=" + password;
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
                var strServerIpAddress = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ServerIPAddress.txt");
                var strSalt = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ServerId.txt");
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
                var strServerIpAddress = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ServerIPAddress.txt");
                var strSalt = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ServerId.txt");
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
                var strServerIpAddress = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ServerIPAddress.txt");
                var strSalt = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ServerId.txt");
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
                var strServerIpAddress = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ServerIPAddress.txt");
                var strSalt = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ServerId.txt");
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



    }
}
