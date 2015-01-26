namespace Hangerd.Bootstrapper
{
	using Hangerd.Components;
	using Hangerd.Extensions;
	using Hangerd.Utility;
	using Microsoft.Practices.Unity;
	using System;
	using System.Linq;

	internal class BootstrapperManager : Disposable
	{
		private readonly IUnityContainer _container;

		public BootstrapperManager(IUnityContainer container)
		{
			_container = container;

			container.RegisterInstance(container);

			BuildManagerWrapper.Current.ConcreteTypes
				.Where(type => typeof(BootstrapperTask).IsAssignableFrom(type))
				.Each(type => container.RegisterMultipleTypesAsSingleton(typeof(BootstrapperTask), type));
		}

		public void Execute()
		{
			_container.ResolveAll<BootstrapperTask>().OrderBy(t => t.Order).Each(task =>
			{
				LocalLoggingService.Info("Bootstrapper begin execute '{0}'", task.GetType().FullName);

				try
				{
					task.Execute();
				}
				catch (Exception ex)
				{
					LocalLoggingService.Exception("Bootstrapper execute error '{0}'，Message：{1}", task.GetType().FullName, ex.ToString());
				}
			});
		}

		protected override void InternalDispose()
		{
			_container.ResolveAll<BootstrapperTask>().OrderByDescending(t => t.Order).Each(task =>
			{
				LocalLoggingService.Info("Bootstrapper begin dispose '{0}'", task.GetType().FullName);

				try
				{
					task.Dispose();
				}
				catch (Exception ex)
				{
					LocalLoggingService.Exception("Bootstrapper dispose error '{0}'，Message：{1}", task.GetType().FullName, ex.ToString());
				}
			});
		}
	}
}
