using System.Data.Entity.ModelConfiguration;
using Hangerd.Domain.Entity;

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
