using Mvc.Mailer;

namespace Mvc3HomeBrewShed.Mailers
{ 
    public interface IUserMailer
    {
            MvcMailMessage Welcome(string firstName, string email);
			MvcMailMessage GoodBye();
            MvcMailMessage PasswordReset(string firstName, string email, string newPassword, string loginUrl);
	}
}