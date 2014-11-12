namespace Hangerd.Mvc.Attributes
{
	using Hangerd.Components;
	using System.Web.Mvc;

	public class ExceptionHandlerAttribute : HandleErrorAttribute
	{
		public override void OnException(ExceptionContext filterContext)
		{
			var controllerName = filterContext.RouteData.Values["controller"];
			var actionName = filterContext.RouteData.Values["action"];

			LocalLoggingService.Exception("Controller:{0}, Action:{1} Error!", controllerName, actionName);
			LocalLoggingService.Exception(filterContext.Exception);

			filterContext.ExceptionHandled = true;

			filterContext.Result = new ViewResult
			{
				ViewName = "_Error"
			};
		}
	}
}