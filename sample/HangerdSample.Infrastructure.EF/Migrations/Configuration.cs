using System.Data.Entity.Migrations;

namespace HangerdSample.Infrastructure.EF.Migrations
{
	internal sealed class Configuration : DbMigrationsConfiguration<HangerdSampleDbContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}
	}
}
