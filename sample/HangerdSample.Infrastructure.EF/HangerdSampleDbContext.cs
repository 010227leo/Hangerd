using System.Data.Entity;
using Hangerd.EntityFramework;
using HangerdSample.Infrastructure.EF.ModelConfigurations;

namespace HangerdSample.Infrastructure.EF
{
	public class HangerdSampleDbContext : HangerdDbContext
	{
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations
				.Add(new AccountConfig());

			base.OnModelCreating(modelBuilder);
		}
	}
}
