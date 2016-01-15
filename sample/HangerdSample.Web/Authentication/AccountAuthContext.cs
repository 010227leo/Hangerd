using System.Web;
using Hangerd.Components;
using Hangerd.Mvc.Authentication;
using HangerdSample.Application.Dtos;
using HangerdSample.Application.Services;

namespace HangerdSample.Web.Authentication
{
	public static class AccountAuthContext
	{
		private const string AccountCachingKey = "HangerdSample.Web.AccountCachingKey";

		public static AccountDto Current
		{
			get
			{
				if (HttpContext.Current == null || !FormsAuthenticationHelper.IsSignIn())
					return null;

				var account = HttpContext.Current.Items[AccountCachingKey] as AccountDto;

				if (account != null)
					return account;

				var accountService = LocalServiceLocator.GetService<IAccountService>();

				account = accountService.Get(FormsAuthenticationHelper.GetUserId());

				if (account != null)
					HttpContext.Current.Items.Add(AccountCachingKey, account);

				return account;
			}
		}
	}
}
