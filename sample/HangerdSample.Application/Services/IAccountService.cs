using System.Collections.Generic;
using Hangerd;
using HangerdSample.Application.Dtos;

namespace HangerdSample.Application.Services
{
	public interface IAccountService
	{
		AccountDto GetAccount(string id);

		HangerdResult<AccountDto> GetAccount(string email, string password);

		IEnumerable<AccountDto> GetAccounts(int pageIndex, int pageSize, out int totalCount);

		HangerdResult<bool> SignUpAccount(AccountDto accountDto);

		HangerdResult<bool> ChangeAccountPassword(string accountId, string oldPassword, string newPassword);

		HangerdResult<bool> RemoveAccount(string accountId);
	}
}
