using System.Web.Mvc;
using System.Web.Routing;
using Hangerd;

namespace HangerdSample.Web
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			RouteConfig.RegisterRoutes(RouteTable.Routes);

			HangerdFramework.Start();
		}

		protected void Application_End()
		{
			HangerdFramework.End();
		}
	}
}
