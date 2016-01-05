using NUnit.Framework;

namespace Hangerd.Test
{
	public class TestBase
	{
		[SetUp]
		protected void TestSetUp()
		{
			HangerdFramework.Start();
		}

		[TearDown]
		protected void TestTearDown()
		{
			HangerdFramework.End();
		}
	}
}
