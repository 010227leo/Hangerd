using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Hangerd.Mvc.Authentication;

namespace Hangerd.Mvc.Attributes
{
	public abstract class LoginAuthAttribute : AuthorizeAttribute
	{
		private readonly string _loginController;
		private readonly string _loginAction;
		private readonly string _loginArea;

		public bool Ajax { get; set; }

		public bool JumpBack { get; set; }

		public string DefaultAjaxResult { get; set; }

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

		protected virtual void RedirectToLoginPage(AuthorizationContext filterContext)
		{
			var routeValue = new RouteValueDictionary
			{
				{"Controller", _loginController},
				{"Action", _loginAction}
			};

			if (_loginArea != null)
				routeValue.Add("Area", _loginArea);

			if (JumpBack)
				routeValue.Add("returnUrl", filterContext.RequestContext.HttpContext.Request.RawUrl);

			filterContext.Result = new RedirectToRouteResult(routeValue);
		}

		protected virtual void ResponseDefaultAjaxResult(AuthorizationContext filterContext)
		{
			filterContext.Result = new ContentResult {Content = DefaultAjaxResult ?? string.Empty};
		}

		protected void ResponseViewResult(AuthorizationContext filterContext, string viewName)
		{
			filterContext.Result = new ViewResult {ViewName = viewName};
		}
	}
}
