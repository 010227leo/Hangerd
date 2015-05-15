using System;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;

namespace Hangerd.Mvc
{
	public class UnityControllerFactory : DefaultControllerFactory
	{
		private readonly IUnityContainer _container;

		public UnityControllerFactory(IUnityContainer container)
		{
			if (container == null)
				throw new ArgumentNullException("container");

			_container = container;
		}

		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			//Try to resolve the controller using asociated IoC container.
			if (controllerType != null)
				return _container.Resolve(controllerType) as IController;

			//Fallback to default behavior.
			return base.GetControllerInstance(requestContext, null);
		}
	}
}
