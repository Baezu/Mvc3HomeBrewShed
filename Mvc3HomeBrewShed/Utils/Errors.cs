using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc3HomeBrewShed.Utils
{   
    /// <summary>
    /// Errors for Account Controller
    /// </summary>
    public enum AccountCreateStatus
    {
        // Summary:
        //     General unknown error.
        Default = 0,
        //
        // Summary:
        //     The user name was not found in the database.
        InvalidUserName = 1,
        //
        // Summary:
        //     The password is not formatted correctly.
        InvalidPassword = 2,
        //
        // Summary:
        //     The password question is not formatted correctly.
        InvalidQuestion = 3,
        //
        // Summary:
        //     The password answer is not formatted correctly.
        InvalidPasswordAnswer = 4,
        //
        // Summary:
        //     The e-mail address is not formatted correctly.
        InvalidEmail = 5,
        //
        // Summary:
        //     The Password Reset is disabled in the web.config for the application.
        PasswordResetDisabled = 6,
        //
        // Summary:
        //     The user was not created, for a reason defined by the provider.
        UserRejected = 7,
        //
        // Summary:
        //     The creating of the new password failed.
        PasswordCreateFailed = 8,
        //
        // Summary:
        //      Password was crested but was not emailed to the user.
        PasswordEmailFailed = 9,
        //
        // Summary:
        //      User account has not been approved.
        UserNotYetApproved = 10,
        //
        // Summary:
        //      User account is locked.
        UserAccountLocked = 11,
    }



}