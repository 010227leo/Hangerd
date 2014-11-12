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
		private IUnityContainer _container;

		public BootstrapperManager(IUnityContainer container)
		{
			this._container = container;

			container.RegisterInstance<IUnityContainer>(container);

			BuildManagerWrapper.Current.ConcreteTypes
				.Where(type => typeof(BootstrapperTask).IsAssignableFrom(type))
				.Each(type => container.RegisterMultipleTypesAsSingleton(typeof(BootstrapperTask), type));
		}

		public void Execute()
		{
			var taskGroups = _container.ResolveAll<BootstrapperTask>().OrderBy(t => t.Order).ToList();

			foreach (var task in taskGroups)
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
			}
		}

		protected override void InternalDispose()
		{
			_container.ResolveAll<BootstrapperTask>().OrderByDescending(t => t.Order).Each(task =>
			{
				try
				{
					LocalLoggingService.Info("Bootstrapper begin dispose '{0}'", task.GetType().FullName);

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
