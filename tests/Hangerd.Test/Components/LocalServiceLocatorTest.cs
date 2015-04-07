using Hangerd.Components;
using Hangerd.Extensions;
using NUnit.Framework;

namespace Hangerd.Test.Components
{
	public class LocalServiceLocatorTest : TestBase
	{
		[Test]
		public void GetServiceForPerRequestTypeTest()
		{
			HangerdFramework.Container.RegisterTypeAsPerRequest<ITestService, TestService>();

			var service1 = LocalServiceLocator.GetService<ITestService>();
			var service2 = LocalServiceLocator.GetService<ITestService>();

			Assert.IsNotNull(service1);
			Assert.IsNotNull(service2);
			Assert.AreEqual(service1, service2);
		}
	}
}
