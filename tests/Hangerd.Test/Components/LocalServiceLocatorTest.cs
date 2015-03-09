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

			var testService1 = LocalServiceLocator.GetService<ITestService>();

			Assert.IsNotNull(testService1);

			var testService2 = LocalServiceLocator.GetService<ITestService>();

			Assert.IsNotNull(testService2);
			Assert.AreEqual(testService1.ServiceId, testService2.ServiceId);
		}
	}
}
