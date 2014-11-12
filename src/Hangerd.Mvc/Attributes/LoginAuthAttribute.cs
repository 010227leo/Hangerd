﻿namespace Hangerd.Mvc.Attributes
{
	using Hangerd.Mvc.Authentication;
	using System.Web;
	using System.Web.Mvc;
	using System.Web.Routing;

	public abstract class LoginAuthAttribute : AuthorizeAttribute
	{
		private string _loginController;
		private string _loginAction;
		private string _loginArea;

		private bool _ajax = false;

		public bool Ajax
		{
			get { return _ajax; }
			set { _ajax = value; }
		}

		private bool _jumpBack = false;

		public bool JumpBack
		{
			get { return _jumpBack; }
			set { _jumpBack = value; }
		}

		private string _defaultAjaxResult = string.Empty;

		public string DefaultAjaxResult
		{
			get { return _defaultAjaxResult; }
			set { _defaultAjaxResult = value; }
		}

		public LoginAuthAttribute(string loginAction, string loginController, string loginArea = null)
		{
			_loginAction = loginAction;
			_loginController = loginController;
			_loginArea = loginArea;
		}

		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			if (!LoginAuthorizeCore(filterContext.HttpContext))
			{
				if (this.Ajax)
				{
					this.ResponseDefaultAjaxResult(filterContext);
				}
				else
				{
					this.RedirectToLoginPage(filterContext);
				}
			}
			else
			{
				OnRolePrivilegeCheck(filterContext);
			}
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
			{
				routeValue.Add("Area", _loginArea);
			}

			if (_jumpBack)
			{
				routeValue.Add("returnUrl", filterContext.RequestContext.HttpContext.Request.RawUrl);
			}

			filterContext.Result = new RedirectToRouteResult(routeValue);
		}

		protected void ResponseDefaultAjaxResult(AuthorizationContext filterContext)
		{
			filterContext.Result = new ContentResult { Content = this.DefaultAjaxResult };
		}

		protected void ResponseViewResult(AuthorizationContext filterContext, string viewName)
		{
			filterContext.Result = new ViewResult { ViewName = viewName };
		}
	}
}
