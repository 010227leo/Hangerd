﻿using Hangerd.Bootstrapper;
using Hangerd.Extensions;
using HangerdSample.Domain.Repositories;
using HangerdSample.Infrastructure.EF.Repositories;
using Microsoft.Practices.Unity;

namespace HangerdSample.Infrastructure.EF.Bootstrapper
{
	public class RegisterServiceTask : RegisterServiceBootstrapperTask
	{
		public RegisterServiceTask(IUnityContainer container) : base(container)
		{
		}

		public override void Execute()
		{
			//DbContext
			_container.RegisterType<HangerdSampleDbContext>();

			//Repository
			_container.RegisterTypeAsSingleton<IAccountRepository, AccountRepository>();
		}
	}
}
