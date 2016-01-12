using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Hangerd;
using Hangerd.Domain.Repository;
using Hangerd.Event.Bus;
using Hangerd.Extensions;
using Hangerd.Uow;
using Hangerd.Validation;
using HangerdSample.Application.Dtos;
using HangerdSample.Domain.Models;
using HangerdSample.Domain.Repositories;
using HangerdSample.Domain.Services;
using HangerdSample.Domain.Specifications;

namespace HangerdSample.Application.Services.Implementation
{
	public class AccountService : HangerdServiceBase, IAccountService
	{
		#region Repository & Services

		private readonly IAccountRepository _accountRepository;
		private readonly IAccountDomainService _accountDomainService;

		#endregion

		public AccountService(
			IAccountRepository accountRepository,
			IAccountDomainService accountDomainService)
		{
			_accountRepository = accountRepository;
			_accountDomainService = accountDomainService;
		}

		public AccountDto GetAccount(string id)
		{
			using (UnitOfWorkManager.Begin<IRepositoryContext>())
			{
				return Mapper.Map<Account, AccountDto>(_accountRepository.Get(id, false));
			}
		}

		public HangerdResult<AccountDto> GetAccountForLogin(string loginName, string password)
		{
			return TryReturn(() =>
			{
				using (UnitOfWorkManager.Begin<IRepositoryContext>())
				{
					if (string.IsNullOrWhiteSpace(loginName) || string.IsNullOrWhiteSpace(password))
						throw new HangerdException("用户名或密码为空");

					var spec = DeletableSpecifications<Account>.NotDeleted() & AccountSpecifications.LoginNameEquals(loginName);
					var account = _accountRepository.Get(spec, false);

					Requires.NotNull(account, "用户名不存在");

					if (!account.ValidatePassword(password))
						throw new HangerdException("密码错误");

					return Mapper.Map<Account, AccountDto>(account);
				}
			}, "登录成功");
		}

		public IEnumerable<AccountDto> GetAccounts(int pageIndex, int pageSize, out int totalCount)
		{
			using (UnitOfWorkManager.Begin<IRepositoryContext>())
			{
				var accounts = _accountRepository.GetAll(DeletableSpecifications<Account>.NotDeleted(), false)
					.OrderByDescending(a => a.CreateTime)
					.Paging(pageIndex, pageSize, out totalCount);

				return Mapper.Map<IEnumerable<Account>, IEnumerable<AccountDto>>(accounts);
			}
		}

		public HangerdResult<bool> RegisterAccount(AccountDto accountDto)
		{
			return TryOperate(() =>
			{
				using (var eventBus = UnitOfWorkManager.Begin<IEventBus>())
				using (var context = UnitOfWorkManager.Begin<IRepositoryContext>())
				{
					var newAccount = _accountDomainService.RegisterNewAccount(
						_accountRepository, accountDto.LoginName, accountDto.Password, accountDto.Name);

					_accountRepository.Add(newAccount);

					context.Commit();
					eventBus.Commit();
				}
			}, "注册成功");
		}

		public HangerdResult<bool> ChangeAccountPassword(string accountId, string oldPassword, string newPassword)
		{
			return TryOperate(() =>
			{
				using (var context = UnitOfWorkManager.Begin<IRepositoryContext>())
				{
					var account = _accountRepository.Get(accountId, true);

					Requires.NotNull(account, "账号信息不存在");
				
					if (!account.ValidatePassword(oldPassword))
						throw new HangerdException("原密码错误");

					account.ChangePassword(newPassword);

					context.Commit();
				}
			});
		}

		public HangerdResult<bool> RemoveAccount(string accountId)
		{
			return TryOperate(() =>
			{
				using (var context = UnitOfWorkManager.Begin<IRepositoryContext>())
				{
					var account = _accountRepository.Get(accountId, true);

					Requires.NotNull(account, "账号信息不存在");

					_accountRepository.Delete(account);

					context.Commit();
				}
			});
		}
	}
}
