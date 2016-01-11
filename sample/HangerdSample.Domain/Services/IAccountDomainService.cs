using HangerdSample.Domain.Models;
using HangerdSample.Domain.Repositories;

namespace HangerdSample.Domain.Services
{
	public interface IAccountDomainService
	{
		/// <summary>
		/// 注册Account
		/// </summary>
		Account RegisterNewAccount(IAccountRepository repository, string loginName, string unencryptedPassword, string name);
	}
}
