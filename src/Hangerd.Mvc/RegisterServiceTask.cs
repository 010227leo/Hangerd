using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hangerd.Bootstrapper;
using Hangerd.Extensions;
using Hangerd.Mvc.Attributes;
using Hangerd.Utility;
using Microsoft.Practices.Unity;

namespace Hangerd.Mvc
{
	public class RegisterServiceTask : RegisterServiceBootstrapperTask
	{
		public RegisterServiceTask(IUnityContainer container)
			: base(container)
		{
		}

		public override int Order
		{
			get { return 1; }
		}

		public override void Execute()
		{
			if (HttpContext.Current == null)
				return;

			RegisterControllers();
			RegisterFactories();
			RegisterFilters();
			ResetViewEngines();
		}

		private void RegisterControllers()
		{
			BuildManagerWrapper.Current.ConcreteTypes
				.Where(type => typeof (IController).IsAssignableFrom(type))
				.Each(type => IocContainer.RegisterType(typeof (IController), type));
		}

		private void RegisterFactories()
		{
			ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory(IocContainer));
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
				AreaViewLocationFormats = new[] { "~/Areas/{2}/Views/{1}/{0}.cshtml", "~/Areas/{2}/Views/Shared/{0}.cshtml" },
				AreaMasterLocationFormats = new[] { "~/Areas/{2}/Views/{1}/{0}.cshtml", "~/Areas/{2}/Views/Shared/{0}.cshtml" },
				AreaPartialViewLocationFormats = new[] { "~/Areas/{2}/Views/{1}/{0}.cshtml", "~/Areas/{2}/Views/Shared/{0}.cshtml" },
				ViewLocationFormats = new[] { "~/Views/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml" },
				MasterLocationFormats = new[] { "~/Views/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml" },
				PartialViewLocationFormats = new[] { "~/Views/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml" },
				FileExtensions = new[] { "cshtml" }
			});
		}
	}
}
