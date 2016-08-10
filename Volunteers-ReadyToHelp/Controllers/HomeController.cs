using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Volunteers_ReadyToHelp.Models;
using System.Net;
using System.Data;
using System.Xml;


namespace Volunteers_ReadyToHelp.Controllers
{
    public class HomeController : Controller
    {
        public AccountController accountController = new AccountController();
        public ActionResult Index()
        {
            //string ipaddress = GetIP();
            //string url = "http://freegeoip.net/json/" + ipaddress.ToString();
            //WebClient client = new WebClient();
            //string jsonstring = client.DownloadString(url);
            //IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            //IPAddress ip = host.AddressList.Where(a => a.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault();
            var a = Request.Browser.Browser;
            var imgsrc = CheckRememberMe();
            Session["ProfilePic"] = imgsrc;
            return View();
        }

        

        public ActionResult Mission()
        {
            CheckRememberMe();
            var imgsrc = CheckRememberMe();
            Session["ProfilePic"] = imgsrc;
            
            return View();
        }

        public ActionResult Services()
        {
            CheckRememberMe();
            var imgsrc = CheckRememberMe();
            Session["ProfilePic"] = imgsrc;
            return View();
        }

        public ActionResult AllActivities()
        {
            CheckRememberMe();
            var imgsrc = CheckRememberMe();
            Session["ProfilePic"] = imgsrc;
            return View();
        }

        public ActionResult ActivityDetails()
        {
            return View();
        }

        /// <summary>
        /// Check is the user is authenticated and sign as remember me before
        /// </summary>
        private string CheckRememberMe()
        {
            var imgsrc = "";
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                imgsrc = accountController.RetriveUserProfilePic(userId);
            }
            return imgsrc;
        }

        private DataTable GetLocation(string ipaddress)
        {
            WebRequest rssReq = WebRequest.Create("http://freegeoip.appspot.com/xml/" + ipaddress);
            WebProxy px = new WebProxy("http://freegeoip.appspot.com/xml/" + ipaddress, true);
            rssReq.Proxy = px;
            rssReq.Timeout = 2000;
            try
            {
                WebResponse rep = rssReq.GetResponse();
                XmlTextReader xtr = new XmlTextReader(rep.GetResponseStream());
                DataSet ds = new DataSet();
                ds.ReadXml(xtr);
                return ds.Tables[0];
            }
            catch
            {
                return null;
            }
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