using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Volunteers_ReadyToHelp.Models;
using System.Collections.Generic;
using Volunteers_ReadyToHelp.ViewModels;
using Facebook;
using reCAPTCHA.MVC;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Configuration;

namespace Volunteers_ReadyToHelp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        
       
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Session["profilePicture"] = "";

            // This doesn't count login failures towards account lockout
            
            //if email is conformed only thaen use can login
            var user = UserManager.FindByEmail(model.Email);
            if (user!= null && !UserManager.IsEmailConfirmed(user.Id))
            {
                return RedirectToAction("EmailNotConfirmed", new { userId = user.Id.ToString() });
            }
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: true);
            if (result == SignInStatus.Success)
            {
                var imgsrc = RetriveUserProfilePicture(user.Id);
                Session["profilePicture"] = imgsrc;
            }
            switch (result)
            {
                case SignInStatus.Success:
                    
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Username or Password is incorrect");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            List<Country> allCountry = new List<Country>();
            List<State> allState = new List<State>();
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                allCountry = dbContext.Country.OrderBy(a => a.CountryName).ToList();
            }
            ViewBag.CountryId = new SelectList(allCountry, "CountryId", "CountryName");
            ViewBag.StateId = new SelectList(allState, "StateId", "StateName");
            return View();
        }
        
        

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegistrationViewModel model, HttpPostedFileBase userImage)
        {
            var response = Request["g-recaptcha-response"];
            string secretKey = ConfigurationManager.AppSettings["reCaptchaSecretKey"];
            var client = new WebClient();
            var captchaResult = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            var obj = JObject.Parse(captchaResult);
            var status = (bool)obj.SelectToken("success");
            List<Country> allCountry = new List<Country>();
            List<State> allState = new List<State>();
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                allCountry = dbContext.Country.OrderBy(a => a.CountryName).ToList();
            }
            ViewBag.CountryId = new SelectList(allCountry, "CountryId", "CountryName");
            ViewBag.StateId = new SelectList(allState, "StateId", "StateName");
            //ViewBag.caPATCHA = status ? "validation success" : "validation failed";
            //var ImageSize = userImage.ContentLength;
            //var ImageType = userImage.ContentType;
            var userAvatarStatus = "";
            if ((userImage != null) && ((userImage.ContentLength < 4200000) && (userImage.ContentType == "image/jpeg" || userImage.ContentType == "image/png" || userImage.ContentType == "image/jpg" || userImage.ContentType == "image/gif" || userImage.ContentType == "image/bmp")))
            {
                userAvatarStatus = "Success";
            }
            else
            {
                ViewBag.InvalidImage = "Invalid Upload";
                return View(model);
            }
            if (ModelState.IsValid && userAvatarStatus == "Success")
                {

                    if (status == false)
                    {
                        ViewBag.reCAPTCHA = "Captcha is invalid";
                        return View();
                    }
                    using (ApplicationDbContext dbContext = new ApplicationDbContext())
                    {
                        var roleId = (from r in dbContext.Roles
                                      where r.Name.Equals(model.RoleType)
                                      select r
                                         );
                        foreach (var item in roleId)
                        {
                            model.RoleId = item.Id;
                        }
                        if (userImage != null)
                        {
                            Avatar avatarModel = new Avatar();
                            avatarModel.AvatarData = new byte[userImage.ContentLength];
                            userImage.InputStream.Read(avatarModel.AvatarData, 0, userImage.ContentLength);
                            Guid id = Guid.NewGuid();
                            avatarModel.AvatarId = id.ToString();
                            model.AvatarId = avatarModel.AvatarId;
                            dbContext.Avatar.Add(avatarModel);
                            dbContext.SaveChanges();
                        }
                        else
                        {
                            model.AvatarId = null;
                        }
                    }


                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, DateOfBirth = model.DOB, CountryId = model.CountryId, StateId = model.StateId, RoleId = model.RoleId, AvatarId = model.AvatarId };
                    var result = await UserManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        Session["profilePicture"] = model.Email;
                        //await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                        //return RedirectToAction("Index", "Home");
                        return RedirectToAction("CheckEmailForNewAccount");
                    }
                    AddErrors(result);
                }
           

            // If we got this far, something failed, redisplay form
            
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult CheckEmailForNewAccount()
        {
            return View();
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            Session["profilePicture"] = "";
            //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            Session["profilePicture"] = "";
            var user = UserManager.FindById(userId);
            //ApplicationDbContext DbContext = new ApplicationDbContext();
            //var userAvatar = (from u in DbContext.Users
            //                  where u.Id.Equals(user.Id)
            //                  join a in DbContext.Avatar
            //                  on u.AvatarId equals a.AvatarId
            //                  select a.AvatarData.ToString()
            //                      );
            //Session["profilePicture"] = userAvatar;
            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            //return View(result.Succeeded ? "ConfirmEmail" : "Error");
            return RedirectToAction("Index","Home");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                return RedirectToAction("ForgotPasswordConfirmation", "Account", new { FirstName = user.FirstName,  });
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            var identity = AuthenticationManager.GetExternalIdentity(DefaultAuthenticationTypes.ExternalCookie);
            var accessToken = identity.FindFirstValue("FacebookAccessToken");
            var fb = new FacebookClient(accessToken);
            dynamic myInfo = fb.Get("/me?fields=email,first_name,last_name,gender,picture,age_range,locale,link,timezone"); // specify the email field
            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            //string value = myInfo.email;
            //string firstname = myInfo.first_name;
            //string lastname = myInfo.last_name;
            //string email = myInfo.email;
            //string picture = myInfo.picture.data.url;
            Session["profilePicture"] = myInfo.picture.data.url;
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    //var a = new ExternalLoginConfirmationViewModel
                    //                            {
                    //                                Email = myInfo.email,
                    //                                FirstName = myInfo.first_name,
                    //                                LastName = myInfo.last_name,
                    //                                ProfilePicture = myInfo.picture,
                    //                                Link = myInfo.link
                    //                            };
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel 
                                                {
                                                    Email = myInfo.email,
                                                    FirstName = myInfo.first_name,
                                                    LastName = myInfo.last_name,
                                                    ProfilePicture = myInfo.picture.data.url,
                                                    Link = myInfo.link
                                                });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { 
                                                    UserName = model.Email, 
                                                    Email = model.Email,
                                                    FirstName = model.FirstName,
                                                    LastName = model.LastName,
                                                    EmailConfirmed = true
                                               };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        //[HttpGet]
        //public ActionResult LogOff()
        //{
        //    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        //    return RedirectToAction("Index", "Home");
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Session["profilePicture"] = null;
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }
        
        [AllowAnonymous]
        public ActionResult Organization()
        {
            return View();
        }

        public async Task<ActionResult> UserProfile(ProfileViewModel model)
        {
            //var a = Session["profilePicture"];
            List<object> myModel = new List<object>();
            var info = await AuthenticationManager.GetExternalLoginInfoAsync();
            var user = UserManager.FindById(User.Identity.GetUserId());
            ApplicationDbContext dbContext = new ApplicationDbContext();
            var userImage = (from u in dbContext.Users
                             where u.AvatarId.Equals(user.AvatarId)
                             join av in dbContext.Avatar
                             on u.AvatarId equals av.AvatarId
                             select av
                            ).ToList();
            foreach (var item in userImage)
            {
                model.AvatarId = item.AvatarId;
            }
            var userDetails = (from u in dbContext.Users
                               where u.Id.Equals(user.Id)
                               select u).ToList();
            myModel.Add(model);
            myModel.Add(userDetails);
            return View(myModel);
        }
        [AllowAnonymous]
        public ActionResult EmailNotConfirmed(ExternalLoginConfirmationViewModel model, string userId)
        {
            var user = UserManager.FindById(userId);
            model.Email = user.Email;
            model.FirstName = user.FirstName;
            
            
            return View(model);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion

        #region RetriveUserProfilePicture
        /// <summary>
        /// This function retrive the image binary date from the database and convert into string helps to represent the image
        /// </summary>
        /// <param name="UserId">Current User Id</param>
        /// <returns></returns>
        public string RetriveUserProfilePicture(string UserId)
        {
            byte[] AvatarBinaryData = null;
            string imgsrc;
            ApplicationDbContext DbContext = new ApplicationDbContext();
            var userAvatar = (from u in DbContext.Users
                              where u.Id.Equals(UserId)
                              join a in DbContext.Avatar
                              on u.AvatarId equals a.AvatarId
                              select a
                              ).ToList();
            foreach (var item in userAvatar)
            {
                AvatarBinaryData = item.AvatarData;
            }
            if (AvatarBinaryData == null)
            {
                imgsrc = "No Image";
            }
            else
            {
                var base64 = Convert.ToBase64String(AvatarBinaryData);
                imgsrc = string.Format("data:image/png;base64,{0}", base64);
            }
            
            return imgsrc;
        }

        #endregion
    }
}