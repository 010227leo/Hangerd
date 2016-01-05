using System;

namespace Hangerd.Test.Components
{
	public class LocalServiceLocatorTest : TestBase
	{
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
