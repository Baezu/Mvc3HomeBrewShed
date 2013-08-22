using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using System.Net.Configuration;
using System.Web.Configuration;

namespace Mvc3HomeBrewShed.Utils
{
    public static class MailClient
    {
        private static readonly SmtpClient Client;
        private static string strFrom;

        private static string addressFrom
	    {
		    get { return strFrom;}
            set { strFrom = value; }
	    }
	

        static MailClient()
        {
            Configuration c = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);

            MailSettingsSectionGroup settings = (MailSettingsSectionGroup) c.GetSectionGroup("system.net/mailSettings");

            MailClient.addressFrom = settings.Smtp.From;

            Client = new SmtpClient
            {
                Host = settings.Smtp.Network.Host,
                Port = Convert.ToInt32(settings.Smtp.Network.Port),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = Convert.ToBoolean(settings.Smtp.Network.EnableSsl)

            };
            Client.UseDefaultCredentials = false;
            Client.Credentials = new NetworkCredential(settings.Smtp.Network.UserName, settings.Smtp.Network.Password);
        }

        /// <summary>
        /// SendMessage
        /// </summary>
        /// <param name="to">Mail To Address</param>
        /// <param name="subject">Email Subject</param>
        /// <param name="body">Message Body</param>
        /// <returns>boolean</returns>
        public static bool SendMessage(string to, string subject, string body)
        {
            MailMessage mm = null;
            bool isSent = false;
            try
            {
                // Create our message
                mm = new MailMessage(addressFrom, to, subject, body);
               // mm.Sender = new MailAddress(addressFrom); 
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mm.IsBodyHtml = true;
                // Send it
                Client.Send(mm);
                isSent = true;
            }
            // Catch any errors, these should be logged and
            // dealt with later
            catch (Exception ex)
            {
                // If you wish to log email errors,
                // add it here...
                var exMsg = ex.Message;
            }
            finally
            {
                mm.Dispose();
            }
            return isSent;
        }

        /// <summary>
        /// SendMessage
        /// </summary>
        /// <param name="from">Email From Address</param>
        /// <param name="to">Email To Address</param>
        /// <param name="subject">Email Subject</param>
        /// <param name="body">Message Body</param>
        /// <returns>boolean</returns>
        public static bool SendMessage(string from, string to, string subject, string body)
        {
            MailMessage mm = null;
            bool isSent = false;
            try
            {
                // Create our message
                mm = new MailMessage(from, to, subject, body);
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mm.IsBodyHtml = true;
                // Send it
                Client.Send(mm);
                isSent = true;
            }
            // Catch any errors, these should be logged and
            // dealt with later
            catch (Exception ex)
            {
                // If you wish to log email errors,
                // add it here...
                var exMsg = ex.Message;
            }
            finally
            {
                mm.Dispose();
            }
            return isSent;
        }


        public static bool SendWelcome(string email)
        {
            string body = "Put welcome email content here...";
            return SendMessage(ConfigurationManager.AppSettings["adminEmail"], email, "Welcome message", body);
        }



        //public void SendEmail(string address, string subject, string message)
        //{
        //    string email = "email4bonnyelder@gmail.com";
        //    string password = "pass@word1";

        //    var loginInfo = new NetworkCredential(email, password);
        //    var msg = new MailMessage();
        //    var smtpClient = new SmtpClient("smtp.gmail.com", 587);

        //    msg.From = new MailAddress(email);
        //    msg.To.Add(new MailAddress(address));
        //    msg.Subject = subject;
        //    msg.Body = message;
        //    msg.IsBodyHtml = true;

        //    smtpClient.EnableSsl = true;
        //    smtpClient.UseDefaultCredentials = false;
        //    smtpClient.Credentials = loginInfo;
        //    smtpClient.Send(msg);
        //}


        //StringBuilder mailBody = new StringBuilder();
        //mailBody.Append("<html><head><style type=\"text\\css\"></style></head>");
        //mailBody.Append("<body>");
        //mailBody.AppendFormat("Hi {0}<br/>", userName);
        //mailBody.AppendFormat("Your reset password is {0}<br/>", password);
        //mailBody.AppendFormat("This password can be changed once you have logged in. <br>");
        //mailBody.AppendFormat("To login go to {0}", loginUrl);
        //mailBody.Append("</body></html>");

        //string message = mailBody.ToString();
        //string subject = "Your new password";

        // Send welcome email
        // MailClient.SendMessage(mailTo, subject, message);

        //SendEmail(mailTo, subject, message);

        //string message = string.Format("Your user name is {0}\r\n", userName);
        //message += string.Format("Your password is {0}\r\n", password);
        //message += "\r\n";
        //message += string.Format("To login go to {0}\r\n", loginUrl);

        // TODO: You need to replace this with your own e-mail code,
        // something along the lines of MyUtilities.EMail.Send(mailTo, subject, message)
        // for now we just fake the e-mail action by logging the message to a text file. 

        //string tempFile = @"c:\\temp\\emaillog.txt";
        //string tempLogMessage = string.Format("to: {0}\r\nsubject: {1}\r\nmessage: {2}\r\n\r\n", mailTo, subject, message);
        //System.IO.File.AppendAllText(tempFile, tempLogMessage);
    }
}