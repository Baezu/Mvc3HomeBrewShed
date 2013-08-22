using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Mvc3HomeBrewShed.Models;
using System.Net;
using System.Net.Mail;
using Mvc3HomeBrewShed.Utils;
using System.Text;
using Mvc3HomeBrewShed.Mailers;
using System.Web.Configuration;


namespace Mvc3HomeBrewShed.Controllers
{
    public class AccountController : Controller
    {

        #region Logon
        // **************************************
        // GET: /Account/LogOn
        // **************************************
        [HttpGet]
        public ActionResult LogOn()
        {
            //-- Sets up database with initial admin user and role
            //-- Set to false in web.config once initialized
            if (Convert.ToBoolean(WebConfigurationManager.AppSettings["AddAdminUserAndRoles"]))
            {
                SetupInitialUser();
            }
            
            return View();
        }
       
        // **************************************
        // POST: /Account/LogOn
        // **************************************

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    MembershipUser user = Membership.GetUser(model.UserName);
                    if (user == null)
                    {
                        ModelState.AddModelError("", ErrorCodeToString(AccountCreateStatus.InvalidUserName));
                    }
                    else
                    {
                        if (!user.IsApproved)
                        {
                            ModelState.AddModelError("", ErrorCodeToString(AccountCreateStatus.UserNotYetApproved));
                        }
                        else if (user.IsLockedOut)
                        {
                            ModelState.AddModelError("", ErrorCodeToString(AccountCreateStatus.UserAccountLocked));
                        }
                        else
                        {
                            ModelState.AddModelError("", ErrorCodeToString(AccountCreateStatus.UserRejected));
                        }
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);           
        }
        #endregion

        #region LogOff

        // **************************************
        // GET: /Account/LogOff
        // **************************************

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
        
        #endregion

        #region Register

        // **************************************
        // GET: /Account/Register
        // **************************************

        public ActionResult Register()
        {
            return View();
        }


        // **************************************
        // POST: /Account/Register
        // **************************************

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, model.PasswordQuestion, model.PasswordAnswer, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    //-- Set initial user role
                    Roles.AddUserToRole(model.UserName, "Brewer");

                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);

                    //Get UserId for registered user
                    var UserId = (Guid)Membership.GetUser(model.UserName).ProviderUserKey;
                    ExtendedUserDetail extrainfo = new ExtendedUserDetail();
                    extrainfo.UserId = UserId;
                    extrainfo.FirstName = model.FirstName;
                    extrainfo.LastName = model.LastName;
                    //Add this info to database
                    ExtendedUserDetailContext db = new ExtendedUserDetailContext();
                    db.ExtendedUserDetails.Add(extrainfo);
                    db.SaveChanges();                    
                    
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        
        #endregion

        #region Change Password

        // **************************************
        // GET: /Account/ChangePassword
        // **************************************

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }


        // **************************************
        // POST: /Account/ChangePassword
        // **************************************

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
                    //-- Password failed
                    ModelState.AddModelError("", ErrorCodeToString(AccountCreateStatus.InvalidPassword));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        
        #endregion

        #region Change Password Success
        // **************************************
        // GET: /Account/ChangePasswordSuccess
        // **************************************

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        } 
        #endregion

        #region Password Reset
        // **************************************
        // URL: /Account/PasswordReset
        // **************************************
        public ActionResult PasswordReset()
        {
            ViewData.Add("PasswordDisabled", false);

            if (!Membership.EnablePasswordReset)
            {
                
                ModelState.AddModelError("", ErrorCodeToString(AccountCreateStatus.PasswordResetDisabled));
                ViewData["PasswordDisabled"] = true;
                
            }

            return View();           
        }
        
        [HttpPost]
        public ActionResult PasswordReset(PasswordResetModel model, string userName)
        {
            if (!Membership.EnablePasswordReset)
            {
                ModelState.AddModelError("", ErrorCodeToString(AccountCreateStatus.PasswordResetDisabled));
                return View(model);
            }

            MembershipUser currentUser = Membership.GetUser(userName);

            if (currentUser == null)
            {
                ModelState.AddModelError("", ErrorCodeToString(AccountCreateStatus.InvalidUserName));
                return View(model);
            }

            if (ModelState.IsValid)
            {
                if (Membership.RequiresQuestionAndAnswer)
                {
                    return RedirectToAction("QuestionAndAnswer", new { userName = userName });
                }
                else
                {
                    RegisterPasswordSuccessModel rpsModel = new RegisterPasswordSuccessModel();
                    rpsModel = ResetPassword(userName, null, GetLoginUrl());

                    if (rpsModel.RegisterPasswordSuccess)
                    {
                        return RedirectToAction("PasswordResetFinal", new { userName = userName });
                    }
                    else
                    {
                        ModelState.AddModelError("", rpsModel.ErrorMessage);
                        return View(model);
                    }
                }
            }           

            // If we got this far, something failed, redisplay form
            return View(model);            
        }
        #endregion

        #region Password reset final
        // **************************************
        // URL: /Account/PasswordResetFinal
        // **************************************
        public ActionResult PasswordResetFinal(string userName)
        {
            if (!Membership.EnablePasswordReset) throw new Exception("Password reset is not allowed\r\n");
            return View();
        }
        #endregion

        #region Question and Answer Section
        // **************************************
        // URL: /Account/QuestionAndAnswer
        // **************************************        
        public ActionResult QuestionAndAnswer(string userName)
        {
            if (!Membership.EnablePasswordReset)
            {
                ModelState.AddModelError("", ErrorCodeToString(AccountCreateStatus.PasswordResetDisabled));
                return View();
            }

            MembershipUser user = Membership.GetUser(userName);
            if (user == null)
            {
                ModelState.AddModelError("", ErrorCodeToString(AccountCreateStatus.InvalidUserName));
                return View();
            }
            else
            {
                ViewData["UserName"] = userName;
                ViewData["Question"] = Membership.GetUser(userName).PasswordQuestion;

                QuestionAndAnswerModel model = new QuestionAndAnswerModel();
                model.UserName = userName;
                model.PasswordQuestion = Membership.GetUser(userName).PasswordQuestion;

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult QuestionAndAnswer(QuestionAndAnswerModel model, string userName, string PasswordQuestion, string PasswordAnswer)
        {            
            if (ModelState.IsValid)
            {
                RegisterPasswordSuccessModel rpsModel = new RegisterPasswordSuccessModel();
                rpsModel = ResetPassword(userName, PasswordAnswer, GetLoginUrl());

                if (rpsModel.RegisterPasswordSuccess)
                {
                    return RedirectToAction("PasswordResetFinal", new { userName = userName });
                }
                else
                {
                    ModelState.AddModelError("", rpsModel.ErrorMessage);
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        #endregion

        #region Forgot Password Methods

        /// <summary>
        /// This allows the non-logged on user to have his password
        /// reset and emailed to him.
        /// </summary>
        /// <returns></returns>
        public ActionResult ForgotPassword()
        {
            var viewModel = new ForgotPasswordViewModel()
            {
                RequiresQuestionAndAnswer = Membership.RequiresQuestionAndAnswer
            };
            return View(viewModel);
        }

        /// <summary>
        /// ForgotPassword
        /// </summary>
        /// <param name="model">ForgotPasswordViewModel model</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            // Get the userName by the email address
            string userName = Membership.GetUserNameByEmail(model.Email);
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("", ErrorCodeToString(AccountCreateStatus.InvalidUserName));
                return View(model);
            }

            // Get the user by the userName
            MembershipUser user = Membership.GetUser(userName);
            if (user == null)
            {
                ModelState.AddModelError("", ErrorCodeToString(AccountCreateStatus.InvalidUserName));
                return View(model);
            }
            else if (!user.IsApproved)
            {
                ModelState.AddModelError("", ErrorCodeToString(AccountCreateStatus.UserNotYetApproved));
                return View(model);
            }
            else if (user.IsLockedOut)
            {
                ModelState.AddModelError("", ErrorCodeToString(AccountCreateStatus.UserAccountLocked));
                return View(model);
            }
            else 
            {
                if (ModelState.IsValid)
                {
                    RegisterPasswordSuccessModel rpsModel = new RegisterPasswordSuccessModel();
                    rpsModel = ResetPassword(userName, model.PasswordAnswer, GetLoginUrl());

                    if (rpsModel.RegisterPasswordSuccess)
                    {
                        return RedirectToAction("ForgotPasswordSuccess", new { userName = userName });
                    }
                    else
                    {
                        ModelState.AddModelError("", rpsModel.ErrorMessage);
                        return View(model);
                    }                    
                }
            }

            return View(model);
        }

        public ActionResult ForgotPasswordSuccess()
        {
            return View();
        }


        #endregion


                
        /// <summary>
        /// ResetPassword
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <param name="passwordAnswer">Password Answer</param>
        /// <param name="loginUrl">Login URL</param>
        /// <returns>RegisterPasswordSuccessModel Model</returns>
        public RegisterPasswordSuccessModel ResetPassword(string userName, string passwordAnswer, string loginUrl)
        {   
            bool resetPasswordSucceeded = false;
            bool changePasswordSucceeded = false;
            bool emailPasswordSucceeded = false;
            string errorMsg = ErrorCodeToString(AccountCreateStatus.Default);
            string resetPasswordNew;

            //-- Set initial model values
            RegisterPasswordSuccessModel rpsModel = new RegisterPasswordSuccessModel();
            rpsModel.ErrorMessage = errorMsg;
            rpsModel.RegisterPasswordSuccess = false;

            if (ModelState.IsValid)
            {
                try
                {
                    if (!Membership.EnablePasswordReset)
                    {
                       rpsModel.ErrorMessage = ErrorCodeToString(AccountCreateStatus.PasswordResetDisabled);
                       return rpsModel;
                    }

                    MembershipUser currentUser = Membership.GetUser(userName);

                    if (currentUser == null)
                    {
                        rpsModel.ErrorMessage = ErrorCodeToString(AccountCreateStatus.InvalidUserName);
                        return rpsModel;
                    }
                    else
                    {
                        //-- Attempt to reset password
                        if (String.IsNullOrEmpty(passwordAnswer))
                        {
                            resetPasswordNew = currentUser.ResetPassword();
                        }
                        else
                        {
                            resetPasswordNew = currentUser.ResetPassword(passwordAnswer);
                        }
                        //-- Check to see if a new password was created.
                        if (String.IsNullOrEmpty(resetPasswordNew))
                        {
                            //-- error creating password
                            resetPasswordSucceeded = false;
                            errorMsg = ErrorCodeToString(AccountCreateStatus.PasswordCreateFailed);
                        }
                        else
                        {
                            // At this point the account has a new randomly generated password.
                            // This is typically a very strong password but almost impossible for 
                            // user to type correctly.
                            //
                            // The code below changes the new password to a human friendly password
                            // (but also much less secure one.) Use this code at your own risk.
                            string friendlyPassword = General.GenerateHumanFriendlyPassword();

                            //-- Update Password with new friendly password
                            changePasswordSucceeded = currentUser.ChangePassword(resetPasswordNew, friendlyPassword);

                            if (!changePasswordSucceeded)
                            {
                                //-- error creating password
                                resetPasswordSucceeded = false;
                                errorMsg = ErrorCodeToString(AccountCreateStatus.PasswordCreateFailed);

                            }
                            else
                            {
                                //-- E-mail the new password to the user.
                                emailPasswordSucceeded = EmailNewPassword(userName, friendlyPassword, GetLoginUrl());
                                if (!emailPasswordSucceeded)
                                {
                                    //-- error creating password
                                    resetPasswordSucceeded = false;
                                    errorMsg = ErrorCodeToString(AccountCreateStatus.PasswordEmailFailed);
                                }
                                else
                                {
                                    //-- everything has been created successfully
                                    resetPasswordSucceeded = true;
                                }
                            }
                        }
                    }
                }
                catch (MembershipPasswordException)
                {
                    resetPasswordSucceeded = false;
                    errorMsg = ErrorCodeToString(AccountCreateStatus.InvalidPasswordAnswer);
                }
                catch (Exception)
                {
                    resetPasswordSucceeded = false;
                }


                //-- Check Reset Password Success
                if (resetPasswordSucceeded)
                {
                    rpsModel.ErrorMessage = "";
                    rpsModel.RegisterPasswordSuccess = true;
                }
                else
                {
                   rpsModel.ErrorMessage = errorMsg;
                }
            }
            return rpsModel;
        }          

        /// <summary>
        /// EmailNewPassword
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <param name="password">Password</param>
        /// <param name="loginUrl">Login URL</param>
        private bool EmailNewPassword(string userName, string password, string loginUrl)
        {
            bool emailNewPasswordSuccess;
            try
            {
                var mailer = new UserMailer();
                string mailTo = Membership.Provider.GetUser(userName, false).Email;

                var msg = mailer.PasswordReset(
                                    firstName: userName,
                                    email: mailTo,
                                    newPassword: password,
                                    loginUrl: loginUrl);

                msg.Send();

                emailNewPasswordSuccess = true;
            }
            catch (Exception)
            {
                emailNewPasswordSuccess = false;                
            }

            return emailNewPasswordSuccess;            
        }

        /// <summary>
        /// GetLoginUrl
        /// </summary>
        /// <returns>Login URL</returns>
        public string GetLoginUrl()
        {
            string thisUrl = Request.Url.AbsoluteUri;
            string baseUrl = thisUrl.Substring(0, thisUrl.LastIndexOf('/'));
            return baseUrl + "/LogOn";
        }
        
        /// <summary>
        /// This function creates initial admin user and admin role.
        /// </summary>
        private void SetupInitialUser()
        {
            if (!Roles.RoleExists("SecurityGuard"))
            {
                Roles.CreateRole("SecurityGuard");
            }

            if (Membership.GetUser("Admin") == null)
            {
                MembershipCreateStatus createStatus;
                Membership.CreateUser("Admin", "password", "brendonely@gmail.com", "colour of sky", "blue", true, out createStatus);
                
                Roles.AddUserToRole("Admin", "SecurityGuard");
            }
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
        
        /// <summary>
        /// Returns errors for Account creation
        /// </summary>
        /// <param name="accountStatus">Account status</param>
        /// <returns>error message</returns>
        private static string ErrorCodeToString(AccountCreateStatus accountStatus)
        {            
            // a full list of status codes.
            switch (accountStatus)
            {
                case AccountCreateStatus.InvalidPasswordAnswer:
                    return "The password answer provided is invalid. Please check the value and try again.";
                    
                case AccountCreateStatus.InvalidPassword:
                    return "The current password is incorrect or the new password is invalid. Please check the value and try again.";

                case AccountCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case AccountCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case AccountCreateStatus.PasswordResetDisabled:
                    return "Password reset is not allowed at this time.";

                case AccountCreateStatus.PasswordCreateFailed:
                    return "There was a problem creating your new password.";

                case AccountCreateStatus.PasswordEmailFailed:
                    return "An error has prevented the emailing of your new password. Please try again.";

                  case AccountCreateStatus.UserNotYetApproved:
                    return "Your account has not been approved yet.";

                  case AccountCreateStatus.UserAccountLocked:
                    return "Your account has been locked. Please contact the System Administrator to unlock your account.";

                  case AccountCreateStatus.UserRejected:
                    return "Your credentials were not accepted. Please check you user name and password and try again.";
                   
                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
