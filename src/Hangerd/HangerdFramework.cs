namespace Hangerd
{
	using Hangerd.Bootstrapper;
	using Hangerd.Components;
	using Microsoft.Practices.Unity;
	using System.Runtime.CompilerServices;

	public class HangerdFramework
	{
		private static readonly IUnityContainer _container = new UnityContainer();
		private static BootstrapperManager _bootstrapperManager;
	
		private HangerdFramework() { }

		public static IUnityContainer Container
		{
			get { return _container; }
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		public static void Start()
		{
			LocalLoggingService.Init();
			LocalLoggingService.Info("Hangerd start!");

			_bootstrapperManager = new BootstrapperManager(_container);
			_bootstrapperManager.Execute();

			LocalLoggingService.Info("Hangerd start complete!");
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		public static void End()
		{
			LocalLoggingService.Info("Hangerd dispose!");

			_bootstrapperManager.Dispose();

			LocalLoggingService.Info("Hangerd dispose complete!");
			LocalLoggingService.Close();
		}
	}
}
