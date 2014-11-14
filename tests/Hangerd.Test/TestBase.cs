namespace Hangerd.Test
{
	using NUnit.Framework;

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
