﻿using System.Web.Mvc;
using Hangerd.Mvc;
using HangerdSample.Web.Authentication;

namespace HangerdSample.Web.Controllers
{
	public class HomeController : HangerdController
	{
		[AccountLoginAuth]
		public ActionResult Index()
		{
			return View();
		}
	}
}
