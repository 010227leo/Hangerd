using System;
using Hangerd.Components;
using Hangerd.Extensions;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Hangerd.Test.Components
{
	public class LocalServiceLocatorTest : TestBase
	{
		[Test]
		public void GetServiceForPerRequestTypeTest()
		{
			HangerdFramework.Container.RegisterTypeAsPerRequest<ITestServiceA, TestServiceA>();

			var service1 = LocalServiceLocator.GetService<ITestServiceA>();
			var service2 = LocalServiceLocator.GetService<ITestServiceA>();

			Assert.IsNotNull(service1);
			Assert.IsNotNull(service2);
			Assert.AreEqual(service1.ServiceId, service2.ServiceId);
		}

		[Test]
		public void GetDependentServiceTest()
		{
			HangerdFramework.Container.RegisterType<ITestServiceA, TestServiceA>();
			HangerdFramework.Container.RegisterType<ITestServiceB, TestServiceB>();

			var service = LocalServiceLocator.GetService<ITestServiceA>();
			var serviceB = LocalServiceLocator.GetService<ITestServiceB>(new ParameterOverrides { { "dependentService", service } });

			Assert.IsNotNull(service);
			Assert.IsNotNull(serviceB);
			Assert.AreEqual(service.ServiceId, serviceB.DependentService.ServiceId);
		}

		private interface ITestServiceA
		{
			string ServiceId { get; }
		}

		private class TestServiceA : ITestServiceA
		{
			private readonly string _serviceId;

			public string ServiceId
			{
				get { return _serviceId; }
			}

			public TestServiceA()
			{
				_serviceId = Guid.NewGuid().ToString("N");
			}
		}

		private interface ITestServiceB
		{
			ITestServiceA DependentService { get; }
		}

		private class TestServiceB : ITestServiceB
		{
			private readonly ITestServiceA _dependentService;

			public ITestServiceA DependentService
			{
				get { return _dependentService; }
			}

			public TestServiceB(ITestServiceA dependentService)
			{
				_dependentService = dependentService;
			}
		}
	}
}
