using Hangerd.EntityFramework.Entity;
using HangerdSample.Domain.Models;

namespace HangerdSample.Infrastructure.EF.ModelConfigurations
{
	public class AccountConfig : EntityTypeConfigBase<Account>
	{
		public AccountConfig()
		{
			Property(a => a.LoginName)
				.IsRequired()
				.HasMaxLength(50);

			Property(a => a.EncryptedPassword)
				.IsRequired()
				.HasMaxLength(32);

			Property(a => a.Name)
				.IsRequired()
				.HasMaxLength(20);
		}
	}
}
