using Hangerd;
using HangerdSample.Domain.Models;
using HangerdSample.Domain.Repositories;

namespace HangerdSample.Domain.Services.Implementation
{
	public class AccountDomainService : IAccountDomainService
	{
		public Account SignUpAccount(IAccountRepository repository, string email, string unencryptedPassword, string name)
		{
			if (repository.ExistLoginName(email))
				throw new HangerdException("Email is already taken");

			return new Account(email, unencryptedPassword, name);
		}
	}
}
