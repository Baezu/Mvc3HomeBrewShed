using Mvc.Mailer;


namespace Mvc3HomeBrewShed.Mailers
{ 
    public class UserMailer : MailerBase, IUserMailer 	
	{
		public UserMailer()
		{
			MasterName="_Layout";
		}
                
        public virtual MvcMailMessage Welcome(string firstName, string email)
        {
            var mailMessage = new MvcMailMessage { Subject = "Welcome" };

            mailMessage.To.Add(email);
            ViewBag.FirstName = firstName;         
            PopulateBody(mailMessage, viewName: "Welcome");

            return mailMessage;
        }
 
		public virtual MvcMailMessage GoodBye()
		{
			//ViewBag.Data = someObject;
			return Populate(x =>
			{
				x.Subject = "GoodBye";
				x.ViewName = "GoodBye";
				x.To.Add("some-email@example.com");
			});
		}

        public virtual MvcMailMessage PasswordReset(string firstName, string email, string newPassword, string loginUrl)
        {
            var mailMessage = new MvcMailMessage { Subject = "Password reset" };
            
            mailMessage.To.Add(email);
            ViewBag.FirstName = firstName;
            ViewBag.NewPassword = newPassword;
            ViewBag.loginUrl = loginUrl;
            PopulateBody(mailMessage, viewName: "PasswordReset");

            return mailMessage;
        }
 	}
}