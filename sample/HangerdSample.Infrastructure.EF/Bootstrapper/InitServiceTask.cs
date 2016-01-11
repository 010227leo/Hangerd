using System.Data.Entity;
using Hangerd.Bootstrapper;
using HangerdSample.Infrastructure.EF.Migrations;
using Microsoft.Practices.Unity;

namespace HangerdSample.Infrastructure.EF.Bootstrapper
{
	public class InitServiceTask : InitServiceBootstrapperTask
	{
		public InitServiceTask(IUnityContainer container) : base(container)
		{
		}

		public override void Execute()
		{
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<HangerdSampleDbContext, Configuration>());
		}
	}
}
