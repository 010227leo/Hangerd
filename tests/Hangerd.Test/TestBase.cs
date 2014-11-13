namespace Hangerd.Test
{
	using NUnit.Framework;

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
