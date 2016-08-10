using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Volunteers_ReadyToHelp.Models;

namespace Volunteers_ReadyToHelp.ServiceLayer
{
    public class ExceptionHandler 
    {
        private ApplicationDbContext dbContext = new ApplicationDbContext();
        private ExceptionLog exceptionLog = new ExceptionLog();
        public void LogException(Exception ex, bool isAuthenticate, string browser)
        {
           
            Guid id = Guid.NewGuid();
            exceptionLog.ErrorId = id.ToString();
            exceptionLog.IP = GetIP();
            exceptionLog.ErrorType = ex.Message;
            exceptionLog.Date = DateTime.Now;
            exceptionLog.Browser = browser;
            exceptionLog.IsUserAuthenticate = isAuthenticate;
            dbContext.ExceptionLog.Add(exceptionLog);
            dbContext.SaveChanges();
        }

        public string GetIP()
        {
            string IP = "";

            string strHostName = "";
            strHostName = System.Net.Dns.GetHostName();

            IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);

            IPAddress[] addr = ipEntry.AddressList;

            IP = addr[1].ToString();

            ////Initializing a new xml document object to begin reading the xml file returned
            //XmlDocument doc = new XmlDocument();
            //doc.Load("http://www.freegeoip.net/json");
            //XmlNodeList nodeLstCity = doc.GetElementsByTagName("City");
            //IP = "" + nodeLstCity[0].InnerText + "<br>" + IP;
            return IP;
        }
    }
}