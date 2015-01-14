namespace Hangerd.Test.Components
{
	using Hangerd.Components;
	using NUnit.Framework;
	using System;

	public class LocalLoggingServiceTest : TestBase
	{
		[Test]
		public void ExceptionLoggingTest()
		{
			try
			{
				var str = string.Empty;
				var str2 = str.Substring(0, 2);

				LocalLoggingService.Debug(str2);

				Assert.Fail();
			}
			catch (ArgumentOutOfRangeException ex)
			{
				LocalLoggingService.Exception(ex);

				Assert.Pass();
			}
		}
	}
}
