using Hangerd.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Hangerd.EntityFramework
{
	public class EntityTypeConfigBase<TEntity> : EntityTypeConfiguration<TEntity>
		where TEntity : EntityBase
	{
		public EntityTypeConfigBase()
		{
			HasKey(e => e.Id);

			Property(e => e.Id)
				.IsRequired()
				.HasMaxLength(32);
		}
	}
}
