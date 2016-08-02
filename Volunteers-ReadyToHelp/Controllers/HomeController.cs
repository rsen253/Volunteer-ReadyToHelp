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
        public ActionResult Index()
        {

            //Session["profilePicture"] = "";
            //if (User.Identity.IsAuthenticated)
            //{
            //    var userId = User.Identity.GetUserId();
            //    var user = UserManager.FindById(userId);
            //    byte[] AvatarBinaryData = null;
            //    ApplicationDbContext DbContext = new ApplicationDbContext();
            //    var userAvatar = (from u in DbContext.Users
            //                      where u.Id.Equals(userId)
            //                      join a in DbContext.Avatar
            //                      on u.AvatarId equals a.AvatarId
            //                      select a
            //                      ).ToList();
            //    foreach (var item in userAvatar)
            //    {
            //        AvatarBinaryData = item.AvatarData;
            //    }
            //    if (AvatarBinaryData == null)
            //    {
            //        Session["profilePicture"] = "No Image";
            //    }
            //    else
            //    {
            //        var base64 = Convert.ToBase64String(AvatarBinaryData);
            //        Session["profilePicture"] = string.Format("data:image/png;base64,{0}", base64);
            //    }
            //}

            return View();
        }

        public ActionResult Mission()
        {
            return View();
        }

        public ActionResult Services()
        {
            return View();
        }

        public ActionResult AllActivities()
        {
            return View();
        }

        public ActionResult ActivityDetails()
        {
            return View();
        }
    }
}