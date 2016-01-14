using System.Web;
using System.Web.Mvc;
using Hangerd.Mvc.Attributes;

namespace HangerdSample.Web.Authentication
{
	public class AccountLoginAuthAttribute : LoginAuthAttribute
	{
		public AccountLoginAuthAttribute()
			: base("SignIn", "Account", string.Empty)
		{
			DefaultAjaxResult = "{\"Success\":false,\"Message\":\"Error\"}";
		}

		protected override bool LoginAuthorizeCore(HttpContextBase httpContext)
		{
			if (!base.LoginAuthorizeCore(httpContext))
				return false;

			return AccountLoginContext.Current != null;
		}

		protected override void OnRolePrivilegeCheck(AuthorizationContext filterContext)
		{
		}
	}
}
