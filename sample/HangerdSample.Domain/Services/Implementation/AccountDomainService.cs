using Hangerd;
using HangerdSample.Domain.Models;
using HangerdSample.Domain.Repositories;

namespace HangerdSample.Domain.Services.Implementation
{
	public class AccountDomainService : IAccountDomainService
	{
		public Account RegisterNewAccount(IAccountRepository repository, string loginName, string unencryptedPassword, string name)
		{
			if (repository.ExistLoginName(loginName))
				throw new HangerdException("登录账号已存在");

			return new Account(loginName, unencryptedPassword, name);
		}
	}
}
