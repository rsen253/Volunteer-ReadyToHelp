using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Volunteers_ReadyToHelp.Models;

namespace Volunteers_ReadyToHelp.Controllers
{
    public class HomeController : Controller
    {
        public AccountController accountController = new AccountController();
        public ActionResult Index()
        {
            
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
    }
}