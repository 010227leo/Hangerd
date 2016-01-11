using Hangerd.Domain.Repository;
using HangerdSample.Domain.Models;

namespace HangerdSample.Domain.Repositories
{
	public interface IAccountRepository : IRepository<Account>
	{
		bool ExistLoginName(string loginName);
	}
}
