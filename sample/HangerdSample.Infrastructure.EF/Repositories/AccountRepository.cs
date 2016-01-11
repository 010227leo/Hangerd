using System.Linq;
using Hangerd.EntityFramework;
using Hangerd.EntityFramework.Repository;
using HangerdSample.Domain.Models;
using HangerdSample.Domain.Repositories;
using HangerdSample.Domain.Specifications;

namespace HangerdSample.Infrastructure.EF.Repositories
{
	public class AccountRepository : EfRepositoryBase<HangerdSampleDbContext, Account>, IAccountRepository
	{
		public AccountRepository(IDbContextProvider<HangerdSampleDbContext> dbContextProvider)
			: base(dbContextProvider)
		{
		}

		public bool ExistLoginName(string loginName)
		{
			var spec = DeletableSpecifications<Account>.NotDeleted() & AccountSpecifications.LoginNameEquals(loginName);

			return GetAll(spec, false).Any();
		}
	}
}
