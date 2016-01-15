using System.Collections.Generic;
using Hangerd;
using HangerdSample.Application.Dtos;

namespace HangerdSample.Application.Services
{
	public interface IAccountService
	{
		AccountDto Get(string id);

		HangerdResult<AccountDto> Get(string email, string password);

		IEnumerable<AccountDto> GetList(int pageIndex, int pageSize, ref int totalCount);

		HangerdResult<bool> SignUp(AccountDto accountDto);

		HangerdResult<bool> ChangePassword(string accountId, string oldPassword, string newPassword);

		HangerdResult<bool> Remove(string accountId);
	}
}
