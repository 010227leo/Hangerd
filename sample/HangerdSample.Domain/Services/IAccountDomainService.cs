using HangerdSample.Domain.Models;
using HangerdSample.Domain.Repositories;

namespace HangerdSample.Domain.Services
{
	public interface IAccountDomainService
	{
		Account SignUpAccount(IAccountRepository repository, string email, string unencryptedPassword, string name);
	}
}
