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

		public HangerdResult<AccountDto> GetAccount(string email, string password)
		{
			return Try(() =>
			{
				using (UnitOfWorkManager.Begin<IRepositoryContext>())
				{
					if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
						throw new HangerdException("Email or password is empty");

					var spec = DeletableSpecifications<Account>.NotDeleted() & AccountSpecifications.LoginNameEquals(email);
					var account = _accountRepository.Get(spec, false);

					Requires.NotNull(account, "Account not exist");

					if (!account.ValidatePassword(password))
						throw new HangerdException("Wrong password");

					return Mapper.Map<Account, AccountDto>(account);
				}
			});
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

		public HangerdResult<bool> SignUpAccount(AccountDto accountDto)
		{
			return Try(() =>
			{
				using (var eventBus = UnitOfWorkManager.Begin<IEventBus>())
				using (var context = UnitOfWorkManager.Begin<IRepositoryContext>())
				{
					var account = _accountDomainService.SignUpAccount(
						_accountRepository, accountDto.LoginName, accountDto.Password, accountDto.Name);

					_accountRepository.Add(account);

					context.Commit();
					eventBus.Commit();
				}
			}, "Success");
		}

		public HangerdResult<bool> ChangeAccountPassword(string accountId, string oldPassword, string newPassword)
		{
			return Try(() =>
			{
				using (var context = UnitOfWorkManager.Begin<IRepositoryContext>())
				{
					var account = _accountRepository.Get(accountId, true);

					Requires.NotNull(account, "Account not exist");
				
					if (!account.ValidatePassword(oldPassword))
						throw new HangerdException("Wrong password");

					account.ChangePassword(newPassword);

					context.Commit();
				}
			});
		}

		public HangerdResult<bool> RemoveAccount(string accountId)
		{
			return Try(() =>
			{
				using (var context = UnitOfWorkManager.Begin<IRepositoryContext>())
				{
					var account = _accountRepository.Get(accountId, true);

					Requires.NotNull(account, "Account not exist");

					_accountRepository.Delete(account);

					context.Commit();
				}
			});
		}
	}
}
