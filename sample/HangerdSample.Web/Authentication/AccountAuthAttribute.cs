using System.Web;
using Hangerd.Mvc.Attributes;

namespace HangerdSample.Web.Authentication
{
	public class AccountAuthAttribute : FormsAuthAttribute
	{
		public AccountAuthAttribute()
			: base("SignIn", "Account")
		{
			DefaultAjaxResult = "{\"Success\":false,\"Message\":\"Error\"}";
		}

		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			if (!base.AuthorizeCore(httpContext))
				return false;

			return AccountAuthContext.Current != null;
		}
	}
}
