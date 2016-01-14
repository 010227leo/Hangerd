using System.Runtime.CompilerServices;
using Hangerd.Bootstrapper;
using Hangerd.Components;
using Microsoft.Practices.Unity;

namespace Hangerd
{
	public static class HangerdFramework
	{
		private static readonly IUnityContainer IocContainer = new UnityContainer();
		private static BootstrapperManager _bootstrapperManager;

		public static IUnityContainer Container
		{
			get { return IocContainer; }
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		public static void Start()
		{
			LocalLoggingService.Init();
			LocalLoggingService.Info("Hangerd start!");

			_bootstrapperManager = new BootstrapperManager(IocContainer);
			_bootstrapperManager.Execute();

			LocalLoggingService.Info("Hangerd start complete!");
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		public static void End()
		{
			LocalLoggingService.Info("Hangerd dispose!");

			_bootstrapperManager.Dispose();

			LocalLoggingService.Info("Hangerd dispose complete!");
			LocalLoggingService.Dispose();
		}
	}
}
