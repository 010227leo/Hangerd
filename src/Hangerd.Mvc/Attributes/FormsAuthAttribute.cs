using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Hangerd.Mvc.Authentication;

namespace Hangerd.Mvc.Attributes
{
	public abstract class FormsAuthAttribute : AuthorizeAttribute
	{
		private readonly string _signinController;
		private readonly string _signinAction;
		private readonly string _signinArea;

		public bool Ajax { get; set; }

		public bool JumpBack { get; set; }

		public string DefaultAjaxResult { get; set; }

		protected FormsAuthAttribute(string signinAction, string signinController, string signinArea = "")
		{
			_signinAction = signinAction;
			_signinController = signinController;
			_signinArea = signinArea;
		}

		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			if (!AuthorizeCore(filterContext.HttpContext))
			{
				if (Ajax)
					ResponseDefaultAjaxResult(filterContext);
				else
					RedirectToSigninPage(filterContext);
			}
			else
				OnRolePrivilegeCheck(filterContext);
		}

		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			return FormsAuthenticationHelper.IsSignIn();
		}

		protected abstract void OnRolePrivilegeCheck(AuthorizationContext filterContext);

		protected virtual void RedirectToSigninPage(AuthorizationContext filterContext)
		{
			var routeValue = new RouteValueDictionary
			{
				{ "Area", _signinArea },
				{ "Controller", _signinController },
				{ "Action", _signinAction }
			};

			if (JumpBack)
				routeValue.Add("returnUrl", filterContext.RequestContext.HttpContext.Request.RawUrl);

			filterContext.Result = new RedirectToRouteResult(routeValue);
		}

		protected virtual void ResponseDefaultAjaxResult(AuthorizationContext filterContext)
		{
			filterContext.Result = new ContentResult { Content = DefaultAjaxResult ?? string.Empty };
		}
	}
}
