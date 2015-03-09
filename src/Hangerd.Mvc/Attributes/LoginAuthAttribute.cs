using Hangerd.Mvc.Authentication;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Hangerd.Mvc.Attributes
{
	public abstract class LoginAuthAttribute : AuthorizeAttribute
	{
		private readonly string _loginController;
		private readonly string _loginAction;
		private readonly string _loginArea;
		private string _defaultAjaxResult = string.Empty;

		public bool Ajax { get; set; }

		public bool JumpBack { get; set; }

		public string DefaultAjaxResult
		{
			get { return _defaultAjaxResult; }
			set { _defaultAjaxResult = value; }
		}

		protected LoginAuthAttribute(string loginAction, string loginController, string loginArea = null)
		{
			_loginAction = loginAction;
			_loginController = loginController;
			_loginArea = loginArea;
		}

		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			if (!LoginAuthorizeCore(filterContext.HttpContext))
			{
				if (Ajax)
					ResponseDefaultAjaxResult(filterContext);
				else
					RedirectToLoginPage(filterContext);
			}
			else
				OnRolePrivilegeCheck(filterContext);
		}

		protected abstract void OnRolePrivilegeCheck(AuthorizationContext filterContext);

		protected virtual bool LoginAuthorizeCore(HttpContextBase httpContext)
		{
			return LoginHelper.IsLogin();
		}

		protected void RedirectToLoginPage(AuthorizationContext filterContext)
		{
			var routeValue = new RouteValueDictionary 
			{ 
				{ "Controller", _loginController }, 
				{ "Action", _loginAction }
			};

			if (!string.IsNullOrWhiteSpace(_loginArea))
				routeValue.Add("Area", _loginArea);

			if (JumpBack)
				routeValue.Add("returnUrl", filterContext.RequestContext.HttpContext.Request.RawUrl);

			filterContext.Result = new RedirectToRouteResult(routeValue);
		}

		protected void ResponseDefaultAjaxResult(AuthorizationContext filterContext)
		{
			filterContext.Result = new ContentResult { Content = DefaultAjaxResult };
		}

		protected void ResponseViewResult(AuthorizationContext filterContext, string viewName)
		{
			filterContext.Result = new ViewResult { ViewName = viewName };
		}
	}
}
