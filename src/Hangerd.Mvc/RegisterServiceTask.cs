namespace Hangerd.Mvc
{
	using Hangerd.Bootstrapper;
	using Hangerd.Extensions;
	using Hangerd.Mvc.Attributes;
	using Hangerd.Utility;
	using Microsoft.Practices.Unity;
	using System.Linq;
	using System.Web;
	using System.Web.Mvc;

	public class RegisterServiceTask : RegisterServiceBootstrapperTask
	{
		public RegisterServiceTask(IUnityContainer container) : base(container) { }

		public override int Order
		{
			get { return 1; }
		}

		public override void Execute()
		{
			if (HttpContext.Current != null)
			{
				RegisterControllers();

				RegisterFactories();

				RegisterFilters();

				ResetViewEngines();
			}
		}

		private void RegisterControllers()
		{
			BuildManagerWrapper.Current.ConcreteTypes
				.Where(type => typeof(IController).IsAssignableFrom(type))
				.Each(type => container.RegisterType(typeof(IController), type));
		}

		private void RegisterFactories()
		{
			ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory(container));
		}

		private static void RegisterFilters()
		{
			GlobalFilters.Filters.Add(new ExceptionHandlerAttribute());
		}

		private static void ResetViewEngines()
		{
			ViewEngines.Engines.Clear();
			ViewEngines.Engines.Add(new RazorViewEngine()
			{
				AreaViewLocationFormats = new string[] { "~/Areas/{2}/Views/{1}/{0}.cshtml", "~/Areas/{2}/Views/Shared/{0}.cshtml" },
				AreaMasterLocationFormats = new string[] { "~/Areas/{2}/Views/{1}/{0}.cshtml", "~/Areas/{2}/Views/Shared/{0}.cshtml" },
				AreaPartialViewLocationFormats = new string[] { "~/Areas/{2}/Views/{1}/{0}.cshtml", "~/Areas/{2}/Views/Shared/{0}.cshtml" },
				ViewLocationFormats = new string[] { "~/Views/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml" },
				MasterLocationFormats = new string[] { "~/Views/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml" },
				PartialViewLocationFormats = new string[] { "~/Views/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml" },
				FileExtensions = new string[] { "cshtml" }
			});
		}
	}
}
