using System.Web.Mvc;
using Hangerd.Components;

namespace Hangerd.Mvc.Attributes
{
	public class ExceptionHandlerAttribute : HandleErrorAttribute
	{
		private const string ErrorViewName = "_Error";

		public override void OnException(ExceptionContext filterContext)
		{
			var controllerName = filterContext.RouteData.Values["controller"];
			var actionName = filterContext.RouteData.Values["action"];

			LocalLoggingService.Exception("Controller:{0}, Action:{1} Error!", controllerName, actionName);
			LocalLoggingService.Exception(filterContext.Exception);

			filterContext.ExceptionHandled = true;

			if (ViewExists(filterContext, ErrorViewName))
			{
				filterContext.Result = new ViewResult
				{
					ViewName = ErrorViewName
				};
			}
			else
			{
				filterContext.Result = new ContentResult
				{
					Content =
@"<!DOCTYPE html>
<html>
<head>
	<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
	<title>Error</title>
</head>
<body>
	<h3>异常日志已被记录，可添加 Shared/_Error.cshtml 自定义错误页面</h3>
</body>"
				};
			}
		}

		private static bool ViewExists(ControllerContext filterContext, string name)
		{
			var result = ViewEngines.Engines.FindView(filterContext, name, null);

			return result.View != null;
		}
	}
}