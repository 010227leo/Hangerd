namespace Hangerd.Mvc
{
	using Microsoft.Practices.Unity;
	using System;
	using System.Web.Mvc;
	using System.Web.Routing;

    public class UnityControllerFactory : DefaultControllerFactory
    {
		private IUnityContainer _container;

		public UnityControllerFactory(IUnityContainer container)
        {
			if (container == (IUnityContainer)null)
			{
				throw new ArgumentNullException("unity container");
			}

            _container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType != (Type)null)
            {
                //Try to resolve the controller using asociated IoC container.
                return _container.Resolve(controllerType) as IController;
            }
            else
            {
                //Fallback to default behavior.
                return base.GetControllerInstance(requestContext, controllerType);
            }
        }
    }
}