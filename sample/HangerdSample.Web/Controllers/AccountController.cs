using System;
using System.Linq;
using System.Web.Mvc;
using Hangerd.Mvc;
using Hangerd.Mvc.Authentication;
using Hangerd.Mvc.ViewModels;
using HangerdSample.Application.Dtos;
using HangerdSample.Application.Services;
using HangerdSample.Web.Authentication;
using HangerdSample.Web.Models;

namespace HangerdSample.Web.Controllers
{
	public class AccountController : HangerdController
    {
		private readonly IAccountService _accountService;

		public AccountController(IAccountService accountService)
		{
			_accountService = accountService;
		}

		#region Sign In

		public ActionResult SignIn()
		{
			if (AccountLoginContext.Current != null)
				return RedirectToAction("Index", "Home");

			return View();
		}

		[HttpPost]
		public ActionResult SignIn(LoginModel model)
		{
			var result = _accountService.Get(model.LoginName, model.Password);
			var success = result.Value != null;

			if (success)
				LoginHelper.Login(result.Value.Id, string.Empty, DateTime.Now.AddHours(2));

			return OperationJsonResult(success, result.Message);
		}

		public ActionResult SignOut()
		{
			LoginHelper.SignOut();

			return RedirectToAction("SignIn");
		}

		#endregion

		#region Sign Up

		public ActionResult SignUp()
		{
			return View();
		}

		[HttpPost]
		public ActionResult SignUp(AccountSignUpModel model)
		{
			if (!ModelState.IsValid)
			{
				var errorMessage = ModelState.Values.First(v => v.Errors.Count > 0).Errors.First().ErrorMessage;

				return JsonContent(new { Success = false, Message = errorMessage });
			}

			var result = _accountService.SignUp(new AccountDto
			{
				LoginName = model.Email,
				Password = model.Password,
				Name = model.Name
			});

			return OperationJsonResult(result);
		}

		#endregion

		[AccountLoginAuth]
		public ActionResult List()
		{
			var totalCount = 0;
			var account = _accountService.GetList(0, 0, ref totalCount);

			ViewBag.TotalCount = totalCount;

			return View(account);
		}

		[AccountLoginAuth(Ajax = true)]
		public ContentResult Remove(string id)
		{
			var result = _accountService.Remove(id);

			return OperationJsonResult(result);
		}
	}
}