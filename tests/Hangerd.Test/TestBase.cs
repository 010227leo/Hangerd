using NUnit.Framework;

namespace Hangerd.Test
{
	public class TestBase
	{
		[SetUp]
		protected virtual void TestSetUp()
		{
			HangerdFramework.Start();
		}

		[TearDown]
		protected virtual void TestTearDown()
		{
			HangerdFramework.End();
		}
	}
}
