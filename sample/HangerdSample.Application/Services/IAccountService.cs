using System.Collections.Generic;
using Hangerd;
using HangerdSample.Application.Dtos;

namespace HangerdSample.Application.Services
{
	public interface IAccountService
	{
		/// <summary>
		/// 根据ID获取Account
		/// </summary>
		AccountDto GetAccount(string id);

		/// <summary>
		/// 根据登录账号、密码获取Account
		/// </summary>
		HangerdResult<AccountDto> GetAccountForLogin(string loginName, string password);

		/// <summary>
		/// 获取Account列表
		/// </summary>
		IEnumerable<AccountDto> GetAccounts(int pageIndex, int pageSize, out int totalCount);

		/// <summary>
		/// 注册Account
		/// </summary>
		HangerdResult<bool> RegisterAccount(AccountDto accountDto);

		/// <summary>
		/// 修改Account密码
		/// </summary>
		HangerdResult<bool> ChangeAccountPassword(string accountId, string oldPassword, string newPassword);

		/// <summary>
		/// 删除Account
		/// </summary>
		HangerdResult<bool> RemoveAccount(string accountId);
	}
}
