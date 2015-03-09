using System;
using System.Web;
using System.Web.Security;

namespace Hangerd.Mvc.Authentication
{
	public static class LoginHelper
	{
		public static bool IsLogin()
		{
			return HttpContext.Current != null && HttpContext.Current.User.Identity.IsAuthenticated;
		}

		public static void SignOut()
		{
			FormsAuthentication.SignOut();
		}

		public static void Login(string userId, string userData, DateTime expiration)
		{
			if (HttpContext.Current == null)
				return;

			var ticket = new FormsAuthenticationTicket(
				1,
				userId,
				DateTime.Now,
				expiration,
				true,
				userData,
				FormsAuthentication.FormsCookiePath);

			var encTicket = FormsAuthentication.Encrypt(ticket);

			HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
		}

		public static string GetUserId()
		{
			return IsLogin() ? HttpContext.Current.User.Identity.Name : string.Empty;
		}

		public static string GetUserData()
		{
			return IsLogin() ? ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.UserData : string.Empty;
		}
	}
}
