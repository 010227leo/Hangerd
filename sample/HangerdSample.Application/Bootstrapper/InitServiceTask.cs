using AutoMapper;
using Hangerd.Bootstrapper;
using Hangerd.Components;
using Hangerd.Event.Bus;
using HangerdSample.Application.Dtos;
using HangerdSample.Domain.Models;
using Microsoft.Practices.Unity;

namespace HangerdSample.Application.Bootstrapper
{
	public class InitServiceTask : InitServiceBootstrapperTask
	{
		public InitServiceTask(IUnityContainer container)
			: base(container)
		{
		}

		public override void Execute()
		{
			ConfigureAutoMapper();

			RegisterEventHandlers();
		}

		private static void ConfigureAutoMapper()
		{
			Mapper.CreateMap<Account, AccountDto>();
		}

		private static void RegisterEventHandlers()
		{
			var dispatcher = LocalServiceLocator.GetService<IEventDispatcher>();

			dispatcher.AutoRegister();
		}
	}
}
