using System;
using System.Web;
using System.Web.Security;

namespace Hangerd.Mvc.Authentication
{
	public static class FormsAuthenticationHelper
	{
		public static void SignOut()
		{
			FormsAuthentication.SignOut();
		}

		public static void SignIn(string userId, string userData, DateTime expiration)
		{
			if (HttpContext.Current == null)
				return;

			var ticket = new FormsAuthenticationTicket(1, userId, DateTime.Now, expiration, true, userData, FormsAuthentication.FormsCookiePath);
			var encryptTicket = FormsAuthentication.Encrypt(ticket);

			HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encryptTicket));
		}

		public static bool IsSignIn()
		{
			return HttpContext.Current != null && HttpContext.Current.User.Identity.IsAuthenticated;
		}

		public static string GetUserId()
		{
			return IsSignIn() ? HttpContext.Current.User.Identity.Name : string.Empty;
		}

		public static string GetUserData()
		{
			return IsSignIn() ? ((FormsIdentity) HttpContext.Current.User.Identity).Ticket.UserData : string.Empty;
		}
	}
}
