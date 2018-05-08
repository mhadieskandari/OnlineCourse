using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BigBlueButton
{
    public class Log
    {
        string strLogFilePath;
        string strLineBreak = "\n========================\n";
        string strLineBreakCustom = "\n*********************************\n\n\n\n";
        string strLineBreakEnd = "\n----------------------------------------------------------\n\n\n";
        FileInfo LogFileInfo;
        long MaxLogFileSize = 0;
        /// <summary>
        /// Log class used to write exception details or 
        /// other specific details into a text file.
        /// </summary>
        /// <param name="strLogFilePath">Full path of the log file including filename</param>
        /// <param name="maxLogFileSize">Maximum Log Size that can be acccomodated on the disk.
        /// (number of Bytes as Long).
        /// Log will be deleted/cleared if size exceeds.
        /// Pass 0 for NO LIMIT on filesize</param>
        public Log(string strLogFilePath, long maxLogFileSize)
        {
            this.MaxLogFileSize = maxLogFileSize;
            this.strLogFilePath = strLogFilePath;
            LogFileInfo = new FileInfo(strLogFilePath);
        }

        private bool CheckLogSize()
        {
            if (MaxLogFileSize != 0)
            {
                if (LogFileInfo.Length > MaxLogFileSize)
                {
                    File.Delete(strLogFilePath);
                    return true;
                }
                else
                {
                    return false;
                }

            }
            return false;
        }
        /// <summary>
        /// Writes exceptions to log files
        /// </summary>
        /// <param name="ex">Pass the exception ex as parameter.</param>
        /// <returns>Returns false if an exception occurs while writing to file</returns>
        public bool Write(Exception ex)
        {
            try
            {
                CheckLogSize();
                if (File.Exists(strLogFilePath))
                {
                    File.AppendAllText(strLogFilePath, DateTime.Now
                                                       + " : Exception :"
                                                       + ex.Message + "\n"
                                                       + "Inner Exception : " + strLineBreak
                                                       + ex.InnerException + "\n"
                                                       + "Stack Trace :" + strLineBreak
                                                       + ex.StackTrace + "\n"
                                                       + "Source : " + strLineBreak
                                                       + ex.Source + strLineBreakEnd);
                    return true;
                }
                else
                {
                    File.WriteAllText(strLogFilePath, DateTime.Now
                                                      + " : Exception :" + strLineBreak
                                                      + ex.Message + "\n"
                                                      + "Inner Exception :" + strLineBreak
                                                      + ex.InnerException + "\n"
                                                      + "Stack Trace :" + strLineBreak
                                                      + ex.StackTrace + "\n"
                                                      + "Source :" + strLineBreak
                                                      + ex.Source + strLineBreakEnd);
                    return true;
                }
            }
            catch { return false; }
        }

        /// <summary>
        /// Write custom strings apart from exceptions
        /// </summary>
        /// <param name="strMessage"></param>
        /// <returns></returns>
        public bool Write(string strMessage)
        {
            try
            {
                if (File.Exists(strLogFilePath))
                {
                    File.AppendAllText(strLogFilePath, strLineBreak
                                                       + DateTime.Now
                                                       + " : " + strMessage + strLineBreakCustom);
                    return true;
                }
                else
                {
                    File.WriteAllText(strLogFilePath, strLineBreak
                                                      + DateTime.Now
                                                      + " : " + strMessage + strLineBreakCustom);
                    return true;
                }
            }
            catch { return false; }
        }



    }




}
