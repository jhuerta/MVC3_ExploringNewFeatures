using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using LearnMVC3.Infrastructure;
using LearnMVC3.Models;
using LearnMVC3.Tests;

namespace LearnMVC3.Controllers
{
    public class AccountController : ApplicationController
    {

        private UserModel _userModel;
        public AccountController() : this(new FormsAuthTokenStore()) { }
        public AccountController(ITokenHandler tokenStore)
            : base(tokenStore)
        {
            _userModel = new UserModel();
        }

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(string email, string password)
        {
            dynamic result = _userModel.Login(email, password);
            if (result.Authenticated)
            {
                SetToken(result.User);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = result.Message;
            }

            // If we got this far, something failed, redisplay form
            return View();
        }

        private void SetToken(dynamic user)
        {
            var token = Guid.NewGuid().ToString();
            _userModel.SetToken(token, user);
            TokenStore.SetClientAccess(token);
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult LogOff()
        {
            TokenStore.RemoveClientAccess();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Register(string email, string password, string confirmPassword)
        {
            var result = _userModel.Register(email, password, confirmPassword);

            if (result.Success)
            {
                SetToken(result.User);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = result.Message;
            }

            // If we got this far, something failed, redisplay form
            return View();
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
